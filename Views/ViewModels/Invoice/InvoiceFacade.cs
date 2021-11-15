using Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace ViewModels
{
    public class InvoiceFacade : BindableBase
    {
        public Invoice Invoice { get; private set; }
        private bool _isSelected;

        public string InvoiceNo
        {
            get { return Invoice.InvoiceNo; }
            set
            {
                if(value != Invoice.InvoiceNo)
                {
                    Invoice.InvoiceNo = value;
                    OnPropertyChanged("InvoiceNo");
                }
            }
        }
        public DateTime Date
        {
            get { return Invoice.Date; }
            set
            {
                if (value != Invoice.Date)
                {
                    Invoice.Date = value;
                    OnPropertyChanged("Date");
                    InvoiceNo = "#01" + value.Month + value.Year;
                }
            }
        }
        public Vessel Vessel
        {
            get { return Invoice.Vessel; }
            set
            {
                if(Vessel != value)
                {
                    Invoice.Vessel = value;
                    OnPropertyChanged("Vessel");
                }
            }
        }

        public int? VesselId
        {
            get { return Invoice.VesselId; }
            set
            {
                if(Invoice.VesselId != value)
                {
                    Invoice.VesselId = value;
                    OnPropertyChanged("VesselId");
                }
            }
        }
        public string BundleName
        {
            get { return Invoice.BundleName; }
            set
            {
                if (value != Invoice.BundleName)
                {
                    Invoice.BundleName = value;
                    OnPropertyChanged("BundleName");
                }
            }
        }
        public double BundlePrice
        {
            get { return Invoice.BundlePrice; }
            set
            {
                if (value != Invoice.BundlePrice)
                {
                    Invoice.BundlePrice = value;
                    OnPropertyChanged("BundlePrice");
                    OnPropertyChanged("Total");
                }
            }
        }
        public double OverMbPrice
        {
            get { return Invoice.OverMbPrice; }
            set
            {
                if (value != Invoice.OverMbPrice)
                {
                    Invoice.OverMbPrice = value;
                    OnPropertyChanged("OverMbPrice");
                    OnPropertyChanged("Total");
                    OnPropertyChanged(nameof(MbPrice));
                }
            }
        }
        public double OverMb
        {
            get { return Invoice.OverMb; }
            set
            {
                if (value != Invoice.OverMb)
                {
                    Invoice.OverMb = value;
                    OnPropertyChanged("OverMb");
                    OnPropertyChanged("Total");
                    OnPropertyChanged(nameof(MbPrice));
                }
            }
        }
        public double OverMinPrice
        {
            get { return Invoice.OverMinPrice; }
            set
            {
                if (value != Invoice.OverMinPrice)
                {
                    Invoice.OverMinPrice = value;
                    OnPropertyChanged("OverMinPrice");
                    OnPropertyChanged("Total");
                    OnPropertyChanged(nameof(MinPrice));
                }
            }
        }
        public double OverMin
        {
            get { return Invoice.OverMin; }
            set
            {
                if (value != Invoice.OverMin)
                {
                    Invoice.OverMin = value;
                    OnPropertyChanged("OverMin");
                    OnPropertyChanged("Total");
                    OnPropertyChanged(nameof(MinPrice));
                }
            }
        }
        public double IntegraPrice
        {
            get { return Invoice.IntegraPrice; }
            set
            {
                if (value != Invoice.IntegraPrice)
                {
                    Invoice.IntegraPrice = value;
                    OnPropertyChanged("IntegraPrice");
                    OnPropertyChanged("Total");
                }
            }
        }
        public bool IsPaid
        {
            get { return Invoice.IsPaid; }
            set
            {
                if(Invoice.IsPaid != value)
                {
                    Invoice.IsPaid = value;
                    OnPropertyChanged("IsPaid");
                }
            }
        }

        public BankAccount Bank
        {
            get { return Invoice.Bank; }
            set
            {
                if(Invoice.Bank != value)
                {
                    Invoice.Bank = value;
                    OnPropertyChanged(nameof(Bank));
                }
            }
        }
        public int? BankId
        {
            get { return Invoice.BankId; }
            set
            {
                if(Invoice.BankId != value)
                {
                    Invoice.BankId = value;
                }
            }
        }
        public double MbPrice
        {
            get
            {
                return Invoice.MbPrice;
            }
        }

        public double MinPrice
        {
            get
            {
                return Invoice.MinPrice;
            }
        }

        public double Total
        {
            get
            {
                return Invoice.Total;
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if(_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public InvoiceFacade(Invoice invoice)
        {
            Invoice = invoice;
            _isSelected = false;
        }

        public InvoiceFacade()
        {
            Invoice = new Invoice();
            _isSelected = false;
        }

    }
}
