using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CompanyEmail
    { 
        public int Id { get; set; }
        public string Email { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public override string ToString()
        {
            return Email;
        }
    }
}
