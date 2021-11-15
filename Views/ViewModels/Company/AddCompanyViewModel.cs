using Utils;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewModels
{
    public class AddCompanyViewModel : BindableBase,ModelViewBase
    {
        private ICompanyService _service;

        private Company _company;
        public string Name
        {
            get { return _company.Name; }
            set
            {
                if (_company.Name != value)
                {
                    _company.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public List<BankAccount> Banks { get; set; }
        public List<string> Emails
        {
            get { return _company.Emails.Select(e => new string(e.Email)).ToList(); }
            set
            {
                if(value != null)
                {
                    _company.Emails = new List<CompanyEmail>();
                    foreach(var email in value)
                    {
                        _company.Emails.Add(new CompanyEmail() { Email = email });
                    }
                }
            }
        }

        public BankAccount SelectedBank { get; set; }


        public AddCompanyViewModel(ICompanyService service)
        {
            _service = service;
            _company = new Company();
            Banks = service.GetAllBankAccounts();

            AddNewCompanyCommand = new RelayCommand(AddNewCompnay);
            GoToCompanyCommand = new RelayCommand(goToCompany);
        }

        public RelayCommand AddNewCompanyCommand { get; set; }
        public RelayCommand GoToCompanyCommand { get; set; }

        private void AddNewCompnay()
        {
            _company.BankId = SelectedBank?.Id;
            _service.AddCompany(_company);
            Mediator.Notify("GoToView", "CompanyView");
        }

        private void goToCompany()
        {
            Mediator.Notify("GoToView", "CompanyView");
        }

    }
}
