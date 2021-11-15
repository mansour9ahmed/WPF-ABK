using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool IsInput { get; set; }
        public string Note { get; set; }
        
        public int AccountId { get; set; }
        public TransactionAccount Account { get; set; }
        public int? BankId { get; set; }
        public BankAccount Bank { get; set; }
    }
}
