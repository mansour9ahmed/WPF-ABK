using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class SoaService : ISoaService
    {
        private BurakDbContext _context;
        public SoaService(BurakDbContext context)
        {
            this._context = context;
        }

        public void CreateSoa(Soa soa)
        {
            if(soa.Company == null && soa.CompanyId == 0)
            {
                throw new Exception("select company");
            }

            if(soa.SoaDate == null)
            {
                throw new Exception("select Date");
            }

            if (soa.SoaNo == null || soa.SoaNo == "")
            {
                throw new Exception("select number");
            }

            _context.Soas.Add(soa);
            _context.SaveChanges();

        }

        public SoaElement CreateSoaElement(SoaElement element)
        {
            var newItem =  _context.SoaElements.Add(element);
            _context.SaveChanges();
            newItem.State = EntityState.Detached;
            return newItem.Entity;

        }

        public void DeleteSoa(int id)
        {
            var soa = _context.Soas.Where(s => s.Id == id).Include(s => s.SoaElements).ThenInclude(e => e.Invoices).FirstOrDefault();

            if(soa == null)
            {
                throw new Exception("Soa not found");
            }

            if(soa.SoaElements != null)
            {
                foreach(var element in soa.SoaElements)
                {
                    if (element.IsActive == false)
                    {
                        if(element.Invoices != null)
                        {
                            foreach(var elin in element.Invoices)
                            {
                                _context.InvoiceSoaElements.Remove(elin);
                            }
                        }

                        _context.SoaElements.Remove(element);
                    }
                }
            }

            _context.Soas.Remove(soa);
            _context.SaveChanges();
        }

        public void DeleteSoaElement(int id)
        {
            var soa = _context.SoaElements.Find(id);

            _context.SoaElements.Remove(soa);
            _context.SaveChanges();
        }

        public void SetPaid(int id)
        {
            var soa = _context.Soas.Where(s => s.Id == id).Include(s => s.SoaElements).ThenInclude(e => e.Invoices).ThenInclude(i => i.Invoice).FirstOrDefault();

            soa.IsPaid = true;
            if(soa.SoaElements != null)
            {
                foreach(var ele in soa.SoaElements)
                {
                    ele.IsPaid = true;
                    if(ele.Invoices != null)
                    {
                        foreach(var invoice in ele.Invoices)
                        {
                            invoice.Invoice.IsPaid = true;
                        }    
                    }
                }
            }

            _context.Update(soa);
            _context.SaveChanges();
        }

        public void UpdateSoa(Soa soa)
        {
            _context.Update(soa);
            _context.SaveChanges();

        }

        public void UpdateSoaElement(SoaElement element)
        {
            _context.Update(element);
            _context.SaveChanges();
        }


        public List<Soa> SearchForSoa(int? companyId, DateTime date, bool? isPiad)
        {
            return _context.Soas.Include(s => s.Company).Where(s => (companyId == null || s.CompanyId == companyId) &&
                (s.SoaDate.Year > date.Year || (s.SoaDate.Year == date.Year && s.SoaDate.Month >= date.Month))
                && (isPiad == null || s.IsPaid == isPiad)).Include(s => s.SoaElements).Include(s => s.Bank).ToList();
        }

        public List<SoaElement> GetElementsByCompany(int companyId)
        {
            List<SoaElement> result = new List<SoaElement>();

            var invoices = _context.Invoices.AsNoTracking().Include(i => i.Vessel).Where(i => i.Vessel.CompanyId == companyId && i.IsPaid == false).
                OrderBy(i => i.Date).Select(i => new { date = new DateTime(i.Date.Year, i.Date.Month, 1), invoice = i }).ToList();
            
            for(int i = 0; i < invoices.Count(); i++)
            {
                DateTime date = invoices.ElementAt(i).date;
                SoaElement element = new SoaElement();
                element.Invoices = new List<InvoiceSoaElement>();

                element.Name = "AIRTIME INVOICE - " + date.ToString("MMM yyyy");
                element.Price = invoices.ElementAt(i).invoice.Total;
                element.Invoices.Add(new InvoiceSoaElement() { InvoiceId = invoices.ElementAt(i).invoice.Id });
                element.CompanyId = companyId;
                element.IsActive = false;


                for (int j = i+1;j<invoices.Count(); j++)
                {
                    var invoice = invoices.ElementAt(j);

                    if(invoice.date == date)
                    {
                        element.Invoices.Add(new InvoiceSoaElement() { InvoiceId = invoice.invoice.Id});
                        element.Price += invoice.invoice.Total;
                    }
                    else
                    {
                        break;
                    }
                    i = j;
                }


                result.Add(element);

            }

            var unpaidElements = _context.SoaElements.Include(s => s.Soa).Include(s => s.Invoices)
                .Where(e => e.CompanyId == companyId && e.IsPaid == false && e.IsActive == true && (e.Invoices == null || e.Invoices.Count == 0))
                .ToList();

            if(unpaidElements != null)
            {
                foreach (var item in unpaidElements)
                {
                    result.Add(item);
                    /*item.IsActive = false;
                    result.Add(new SoaElement()
                    {
                        Name = item.Name,
                        Price = item.Price,
                        IsActive = true,
                        IsPaid = false,
                        CompanyId = item.CompanyId
                    });
                    */
                }
            }
            

            return result;
        }

        public List<SoaElement> GetPurchasesByCompany(int companyId, bool? isPaid = false)
        {
            return _context.SoaElements.Include(s => s.Invoices)
                .Where(e => e.CompanyId == companyId && ( isPaid == null || e.IsPaid == isPaid) && e.IsActive == true && (e.Invoices == null || e.Invoices.Count == 0)).AsNoTracking()
                .ToList();
        }

    }
}
