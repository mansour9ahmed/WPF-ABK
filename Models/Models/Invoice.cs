using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public string BundleName { get; set; }
        public double BundlePrice { get; set; }
        public double OverMb { get; set; }
        public double OverMbPrice { get; set; }
        public double OverMin { get; set; }
        public double OverMinPrice { get; set; }
        public double IntegraPrice { get; set; }
        public bool IsPaid { get; set; }

        // foriegn keys
        public int? VesselId { get; set; }
        public Vessel Vessel { get; set; }

        public int? BankId { get; set; }
        public BankAccount Bank { get; set; }

        public List<InvoiceSoaElement> SoaElements { get; set; }


        [NotMapped]
        public double Total
        {
            get { return BundlePrice + IntegraPrice + MbPrice + MinPrice; }
        }

        [NotMapped]
        public double MbPrice
        {
            get { return OverMbPrice * OverMb; }
        }

        [NotMapped]
        public double MinPrice
        {
            get { return OverMinPrice * OverMin; }
        }


    }
}
