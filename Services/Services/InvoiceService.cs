using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class InvoiceService : IInvoiceService
    {
        private BurakDbContext _context;

        public InvoiceService(BurakDbContext context)
        {
            this._context = context;
        }
        public void CreateInvoice(Invoice invoice)
        {

            if(invoice.VesselId == null)
            {
                throw new Exception("Please select Vessel");
            }

            if(invoice.Date == null)
            {
                throw new Exception("Please select Date");
            }

            if(invoice.InvoiceNo == null || invoice.InvoiceNo == "")
            {
                throw new Exception("Invoice Number cannot be empty");
            }
        
   
            var entity = _context.Add(invoice);
            _context.SaveChanges();
        }

        public bool DeleteInvoice(int id)
        {
            var invoice = _context.Invoices.Find(id);

            if(invoice == null)
            {
                throw new Exception("Invoice not found");
            }

            _context.Invoices.Remove(invoice);
            _context.SaveChanges();

            return true;
        }

        public List<Invoice> SearchForInvoices(DateTime FromDate, DateTime ToDate, bool? isPaid, int? VesselId, int? CompanyId)
        {
            
            return _context.Invoices.Where(i => (FromDate == null || i.Date.Year > FromDate.Year || (i.Date.Year == FromDate.Year && i.Date.Month >= FromDate.Month))
                                            && (ToDate == null || i.Date.Year < ToDate.Year || (i.Date.Year == ToDate.Year && i.Date.Month <= ToDate.Month))
                                            && (!isPaid.HasValue || i.IsPaid == isPaid) && (!VesselId.HasValue|| VesselId == 0 || i.VesselId == VesselId)
                                            && (!CompanyId.HasValue || CompanyId == 0 || CompanyId == i.Vessel.CompanyId)
            ).Include(i => i.Bank).Include(i => i.Vessel).ThenInclude(v => v.Company).OrderBy(i => i.Date).ToList();
        }


        public void UpdateInvoice(Invoice invoice)
        {
            bool isPaid = _context.Invoices.AsNoTracking().Where(i => i.Id == invoice.Id).Select(c => c.IsPaid).FirstOrDefault();

            if (isPaid) 
            {
                throw new Exception("the invoice is already paid, you cannot edit it");
            }

            if (invoice.VesselId == null)
            {
                throw new Exception("Please select Vessel");
            }

            if (invoice.Date == null)
            {
                throw new Exception("Please select Date");
            }

            if (invoice.InvoiceNo == null || invoice.InvoiceNo == "")
            {
                throw new Exception("Invoice Number cannot be empty");
            }

            var entity = _context.Update(invoice);
            
            _context.SaveChanges();

        }


        public bool SetPaid(int invoiceId)
        {
           var invoice =  _context.Invoices.Find(invoiceId);
            
            if(invoice == null)
            {
                throw new Exception("Invoice is not existed");
            }

            if (invoice.IsPaid)
            {
                throw new Exception("Invoice is already paid");
            }

            invoice.IsPaid = true;

            _context.Update(invoice);
            _context.SaveChanges();

            return true;
        }


        public List<BankAccount> GetAllBankAccounts()
        {
            return _context.BankAccounts.AsNoTracking().ToList();
        }


    }
}
