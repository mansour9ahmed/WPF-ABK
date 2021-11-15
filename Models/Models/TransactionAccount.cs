using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class TransactionAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Transaction> Transactions { get; set; }

        [NotMapped]
        public double Total
        {
            get
            {
                double total = 0;
                if(Transactions != null)
                {
                    foreach(var item in Transactions)
                    {
                        if (item.IsInput)
                        {
                            total += item.Amount;
                        }
                        else
                        {
                            total -= item.Amount;
                        }
                    }
                }

                return total;
            }
        }

    }
}
