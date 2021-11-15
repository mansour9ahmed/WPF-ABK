using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private BurakDbContext _context;

        public TransactionService(BurakDbContext context)
        {
            this._context = context;
        }

        public TransactionAccount CreateAccount(TransactionAccount account)
        {
            if(account.Name == null || account.Name == "")
            {
                throw new Exception("Please insert name");
            }

            if(_context.TransactionAccounts.Any(a => a.Name == account.Name))
            {
                throw new Exception("Name is already existed");
            }


            var entity = _context.TransactionAccounts.Add(account);
            _context.SaveChanges();

            entity.State = EntityState.Detached;

            return entity.Entity;
        }

        public Transaction CreateTransaction(Transaction transaction)
        {
            if(transaction.AccountId == 0 && transaction.Account == null)
            {
                throw new Exception("please select account");
            }

            var ent = _context.Transactions.Add(transaction);
            _context.SaveChanges();

            ent.State = EntityState.Detached;

            return ent.Entity;

        }

        public void DeleteAccount(int id)
        {
            var account = _context.TransactionAccounts.Find(id);
            if(account == null)
            {
                throw new Exception("Account is not existed");
            }

            _context.TransactionAccounts.Remove(account);
            _context.SaveChanges();

        }

        public void DeleteTransaction(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null)
            {
                throw new Exception("Transaction is not existed");
            }

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }

        public TransactionAccount GetAccount(int id)
        {
            return _context.TransactionAccounts.Where(a => a.Id == id).Include(a => a.Transactions).ThenInclude(t => t.Bank).AsNoTracking().FirstOrDefault();
        }

        public List<TransactionAccount> GetAllAccounts()
        {
           return _context.TransactionAccounts.Include(a => a.Transactions).ToList();
        }

        public List<BankAccount> GetAllBankAccounts()
        {
            return _context.BankAccounts.ToList();
        }

        public List<Transaction> GetTransactionsByAccount(int id)
        {
            if(!_context.TransactionAccounts.Any(a => a.Id == id))
            {
                throw new Exception("Account is not existed");
            }

            return _context.Transactions.Where(t => t.AccountId == id).OrderBy(t => t.IsInput).OrderBy(t => t.Date).Include(t => t.Account).Include(t => t.Bank).AsNoTracking().ToList();
        }

        public void UpdateAccount(TransactionAccount account)
        {
            if (account.Name == null || account.Name == "")
            {
                throw new Exception("Please insert name");
            }

            if (_context.TransactionAccounts.Any(a => a.Name == account.Name))
            {
                throw new Exception("Name is already existed");
            }

            _context.Update(account);
            _context.SaveChanges();
        }
    }
}
