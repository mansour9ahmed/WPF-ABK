using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface ICompanyService
    {
        List<Company> GetAllCompanies();
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(int id);

        List<BankAccount> GetAllBankAccounts();
    }
}
