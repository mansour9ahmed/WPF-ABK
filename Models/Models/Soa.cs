using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Soa
    {
        public int Id { get; set; }
        public string SoaNo { get; set; }
        public DateTime SoaDate { get; set; }
        public bool IsPaid { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int? BankId { get; set; }
        public BankAccount Bank { get; set; }
        public List<SoaElement> SoaElements { get; set; }

        [NotMapped]
        public double Total
        {
            get
            {
                double total = 0;
                if(SoaElements != null)
                {
                    foreach(var tmp in SoaElements)
                    {
                        total += tmp.Price;
                    }
                }
                return total;
            }
        }

    }
}
