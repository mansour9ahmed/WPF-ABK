using Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace ViewModels
{
    public class VesselFacade : BindableBase
    {
        public Vessel vessel { get; }
        public string Name
        {
            get { return vessel.Name; }
            set
            {
                if (vessel.Name != value)
                {
                    vessel.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Email
        {
            get { return vessel.Email; }
            set
            {
                if (vessel.Email != value)
                {
                    vessel.Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public string SimNo
        {
            get { return vessel.SimNo; }
            set
            {
                if (vessel.SimNo != value)
                {
                    vessel.SimNo = value;
                    OnPropertyChanged("SimNo");
                }
            }
        }

        public string SimType
        {
            get { return vessel.SimType; }
            set
            {
                if (vessel.SimType != value)
                {
                    vessel.SimType = value;
                    OnPropertyChanged("SimType");
                }
            }
        }
        public DateTime ActivationDate
        {
            get { return vessel.ActivationDate; }
            set
            {
                if (vessel.ActivationDate != value)
                {
                    vessel.ActivationDate = value;
                    OnPropertyChanged("SimType");
                }
            }
        }
        public string BundleName
        {
            get { return vessel.BundleName; }
            set
            {
                if (vessel.BundleName != value)
                {
                    vessel.BundleName = value;
                    OnPropertyChanged("BundleName");
                }
            }
        }
        public Company Company
        {
            get { return vessel.Company; }
            set
            {
                if (vessel.CompanyId != value.Id)
                {
                    vessel.CompanyId = value.Id;
                    OnPropertyChanged("Company");
                }
            }
        }
        public string AccountPassword
        {
            get { return vessel.AccountPassword; }
            set
            {
                if (vessel.AccountPassword != value)
                {
                    vessel.AccountPassword = value;
                    OnPropertyChanged("AccountPassword");
                }
            }
        }
        public string MsgSize
        {
            get { return vessel.MsgSize; }
            set
            {
                if (vessel.MsgSize != value)
                {
                    vessel.MsgSize = value;
                    OnPropertyChanged("MsgSize");
                }
            }
        }
        public double BundlePrice
        {
            get { return vessel.BundlePrice; }
            set
            {
                if (vessel.BundlePrice != value)
                {
                    vessel.BundlePrice = value;
                    OnPropertyChanged("BundlePrice");
                }
            }
        }
        public double IntegraPrice
        {
            get { return vessel.IntegraPrice; }
            set
            {
                if (vessel.IntegraPrice != value)
                {
                    vessel.IntegraPrice = value;
                    OnPropertyChanged("IntegraPrice");
                }
            }
        }
        public double OverMbPrice
        {
            get { return vessel.OverMbPrice; }
            set
            {
                if (vessel.OverMbPrice != value)
                {
                    vessel.OverMbPrice = value;
                    OnPropertyChanged("OverMbPrice");
                }
            }
        }
        public double OverMinPrice
        {
            get { return vessel.OverMinPrice; }
            set
            {
                if (vessel.OverMinPrice != value)
                {
                    vessel.OverMinPrice = value;
                    OnPropertyChanged("OverMinPrice");
                }
            }
        }

        public VesselFacade(Vessel vessel)
        {
            this.vessel = vessel;
        }

        public VesselFacade()
        {
            vessel = new Vessel();
        }
    }
}
