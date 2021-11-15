using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class SoaFacade : BindableBase
    {
        public Soa Soa { get; }

        public int Id
        {
            get { return Soa.Id; }
            set
            {
                if(Soa.Id != value)
                {
                    Soa.Id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public Company Company
        {
            get { return Soa.Company; }
            set
            {
                if(Soa.Company != value)
                {
                    Soa.Company = value;
                    OnPropertyChanged("Company");
                }
            }
        }
        public int CompanyId
        {
            get { return Soa.CompanyId; }
            set
            {
                if(value != Soa.CompanyId)
                {
                    Soa.CompanyId = value;
                }
            }
        }
        public DateTime SoaDate
        {
            get { return Soa.SoaDate; }
            set
            {
                if(Soa.SoaDate != value)
                {
                    Soa.SoaDate = value;
                    OnPropertyChanged("SoaDate");
                    SoaNo = "#02" + value.Month + value.Year;
                }
            }
        }

        public string SoaNo
        {
            get { return Soa.SoaNo; }
            set
            {
                if(Soa.SoaNo != value)
                {
                    Soa.SoaNo = value;
                    OnPropertyChanged(nameof(SoaNo));
                }
            }
        }
        public double Total
        {
            get
            {
                double total = 0.0;
                if(Soa.SoaElements != null)
                {
                    foreach(var ele in Soa.SoaElements)
                    {
                        total += ele.Price;
                    }
                }
                return total;
            }
        }

        public bool IsPaid
        {
            get { return Soa.IsPaid; }
            set
            {
                if(Soa.IsPaid != value)
                {
                    Soa.IsPaid = value;
                    OnPropertyChanged(nameof(IsPaid));
                }
            }
        }

        public bool IsSelected { get; set; }
        public List<SoaElement> SoaElements
        {
            get { return Soa.SoaElements; }
            set
            {
                if(Soa.SoaElements != value)
                {
                    Soa.SoaElements = value;
                    OnPropertyChanged(nameof(SoaElements));
                }
            }
        }

        public int? BankId
        {
            get { return Soa.BankId; }
            set
            {
                if(Soa.BankId != value)
                {
                    Soa.BankId = value;
                }
            }
        }
        public BankAccount Bank
        {
            get { return Soa.Bank; }
            set
            {
                if(Soa.Bank != value)
                {
                    Soa.Bank = value;
                }
            }
        }

        public SoaFacade()
        {
            Soa = new Soa();
        }

        public SoaFacade(Soa soa)
        {
            Soa = soa;
        }
    }
}
