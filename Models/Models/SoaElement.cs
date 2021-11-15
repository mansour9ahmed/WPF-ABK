using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SoaElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsPaid { get; set; }
        public bool? IsActive { get; set; } = false;

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int? SoaId { get; set; }
        public Soa Soa { get; set; }
        public List<InvoiceSoaElement> Invoices { get; set; }
    }
}
