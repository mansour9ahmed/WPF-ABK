using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class InvoiceSoaElement
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int  SoaElementId { get; set; }
        public SoaElement SoaElement { get; set; }
    }
}
