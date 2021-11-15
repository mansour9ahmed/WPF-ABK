using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Vessel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SimNo { get; set; }
       public string SimType { get; set; }
       public DateTime ActivationDate { get; set; }
        public string Email { get; set; }
        public string BundleName{ get; set; }
        public double BundlePrice { get; set; }
        public double OverMbPrice { get; set; }
        public double OverMinPrice { get; set; }
        public double IntegraPrice { get; set; }
        public string AccountPassword { get; set; }
        public string MsgSize { get; set; }

        // foreign keys
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<Invoice> Invoices { get; set; }
    }
}
