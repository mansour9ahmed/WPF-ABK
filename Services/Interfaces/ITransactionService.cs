using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ITransactionService
    {
        List<TransactionAccount> GetAllAccounts();
        TransactionAccount GetAccount(int id);
        List<Transaction> GetTransactionsByAccount(int id);
        TransactionAccount CreateAccount(TransactionAccount account);
        Transaction CreateTransaction(Transaction transaction);
        void UpdateAccount(TransactionAccount account);
        void DeleteAccount(int id);
        void DeleteTransaction(int id);
        List<BankAccount> GetAllBankAccounts();
    }
}
