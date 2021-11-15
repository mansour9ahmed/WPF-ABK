
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CompanyEmail> Emails { get; set; }
        public List<Vessel> Vessels { get; set; }
        public List<SoaElement> Elements { get; set; }
        public int? BankId { get; set; }
        public BankAccount Bank { get; set; }
    }
}
