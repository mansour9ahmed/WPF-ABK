using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        BurakDbContext _context;

        public CompanyService(BurakDbContext context)
        {
            this._context = context;
        }

        public void AddCompany(Company company)
        {
            if (company.Name == null || company.Name == "")
            {
                throw new Exception("Name can't be empty");
            }
            if (_context.Companies.Any(c => c.Name == company.Name))
            {
                throw new Exception("The Company is already exists");
            }

            var entity = _context.Companies.Add(company);
            _context.SaveChanges();
            entity.State = EntityState.Detached;
        }

        public void DeleteCompany(int id)
        {
            if(!_context.Companies.Any(c => c.Id == id))
            {
                throw new Exception("The company is not existed");
            }

            Company company = _context.Companies.Where(c => c.Id == id).Include(c => c.Vessels).FirstOrDefault();

            if (company.Vessels != null && company.Vessels.Count > 0)
            {
                throw new Exception("The Company has related Vessels, please delete its vessels first.");
            }
            _context.Companies.Remove(company);
            _context.SaveChanges();
        }

        public List<Company> GetAllCompanies()
        {
            return _context.Companies.AsNoTracking().Include(c => c.Emails).Include(c => c.Bank).Include(c => c.Vessels).OrderBy(c => c.Name).ToList();
        }

        public void UpdateCompany(Company company)
        {
            if(!_context.Companies.Any(c => c.Id == company.Id))
            {
                throw new Exception("Compnay is not existed, please create a new one.");
            }    

            if(company.Name == null || company.Name == "")
            {
                throw new Exception("Name can't be empty");
            }
            if (_context.Companies.Any(c => c.Name == company.Name && c.Id != company.Id))
            {
                throw new Exception("The Company is already exists");
            }
            var entity = _context.Companies.Update(company);
            _context.SaveChanges();
            entity.State = EntityState.Detached;
            
        }

        public List<BankAccount> GetAllBankAccounts()
        {
            return _context.BankAccounts.AsNoTracking().ToList();
        }
    }
}
