using Utils;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class UpdateCompanyViewModel : BindableBase,ModelViewBase
    {
        private Company _company;
        private ICompanyService _service;

        public string Name {
            get { return _company.Name; }
            set { if(_company.Name != value)
                {
                    _company.Name = value;
                    OnPropertyChanged(nameof(Name));
                } }
        }
        public List<string> Emails {
            get { return _company.Emails.Select(e=>e.Email).ToList(); }
            set
            {
                if(value != null)
                {
                    _company.Emails.Clear();
                    foreach(var email in value)
                        _company.Emails.Add(new CompanyEmail() { Email = email });
                }
            }
        }


        public List<BankAccount> Banks { get; set; }
        public BankAccount SelectedBank { get; set; }

        public UpdateCompanyViewModel(ICompanyService service,Company company)
        {
            _company = company;
            _service = service;

            Banks = service.GetAllBankAccounts();
            SelectedBank = Banks.Find(b => b.Id == _company.BankId);

            GoToCompanyCommand = new RelayCommand(goToCompany);
            UpdateCompanyCommand = new RelayCommand(UpdateCompnay);
        }

        public RelayCommand GoToCompanyCommand { get; set; }
        public RelayCommand UpdateCompanyCommand { get; set; }

        private void goToCompany()
        {
            Mediator.Notify("GoToView", "CompanyView");
        }

        private void UpdateCompnay()
        {
            _company.BankId = SelectedBank.Id;   
            _service.UpdateCompany(_company);
            Mediator.Notify("GoToView", "CompanyView");
        }
    }
}
