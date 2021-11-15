using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string BeneficiaryName { get; set; }
        public string UsdAccountNo { get; set; }
        public string UsdIban { get; set; }
        public string EuroAccountNo { get; set; }
        public string EuroIban { get; set; }
        public string Swift { get; set; }

        public List<Company> Companies { get; set; }
        public List<Soa> Soas { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
