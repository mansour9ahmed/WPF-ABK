using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ViewModels
{
    public class CompanyViewModel : BaseViewModel
    {

        #region Private members

        private ICompanyService _service;
        #endregion


        #region Properties
        public ObservableCollection<Company> Companies { get; set; }
        public Company SelectedCompany { get; set; }
        public Company NewCompany { get; set; }

        #endregion


        #region Constructor
        public CompanyViewModel(ICompanyService service)
        {
            _service = service;

            AddNewCompanyCommand = new RelayCommand<string>(AddNewCompany);

            NewCompany = new Company();
            Companies = new ObservableCollection<Company>();
            foreach (var item in _service.GetAllCompanies())
            {
                Companies.Add(item);
            }
        }

        #endregion


        #region Commands
        public RelayCommand<string> AddNewCompanyCommand { get; set; }

        #endregion


        #region Command functions

        public void AddNewCompany(string emailsStr)
        {
            List<string> emails = emailsStr.Split(',').ToList();

            NewCompany.Emails = new List<CompanyEmail>();
            foreach (var email in emails)
            {
                NewCompany.Emails.Add(new CompanyEmail
                {
                    Email = email
                });
            }

            _service.AddCompany(NewCompany);
            NewCompany = new Company();
        }

        #endregion


    }
}
