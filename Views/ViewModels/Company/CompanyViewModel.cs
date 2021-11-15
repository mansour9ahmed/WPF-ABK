using Utils;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Linq;

namespace ViewModels
{
    public class CompanyViewModel : BindableBase,ModelViewBase
    {
        private ICompanyService _service;
        private Company _selectedCompnay;
        public Company SelectedCompany
        {
            get { return _selectedCompnay; }
            set
            {
                SetProperty(ref _selectedCompnay, value);
                OnPropertyChanged("Emails");
            }
        }

        public List<string> Emails
        {
            get
            {  
                if(SelectedCompany != null)
                    return _selectedCompnay.Emails.Select(c => c.Email).ToList();
                return null;
            }
        }

        public ObservableCollection<Company> Companies { get; set; } = new ObservableCollection<Company>();

        public CompanyViewModel(ICompanyService service)
        {
            _service = service;
            foreach(var company in _service.GetAllCompanies())
            {
                Companies.Add(company);
            }

            GoToAddCommand = new RelayCommand(goToAdd);
            GoToUpdateCompanyCommand = new RelayCommand<Company>(goToUpdateCompnay);
            DeleteCompanyCommand = new RelayCommand<Company>(deleteComanpy);

            SelectedCompany = null;
        }


        // commands
        public RelayCommand GoToAddCommand { get; set; }
        public RelayCommand<Company> GoToUpdateCompanyCommand { get; set; }
        public RelayCommand<Company> DeleteCompanyCommand { get; set; }

        private void goToAdd()
        {
            Mediator.Notify("GoToView", "AddCompanyView");
        }

        private void goToUpdateCompnay(Company com)
        {
            Mediator.Notify("GoToView", "UpdateCompanyView", com);
        }

        private void deleteComanpy(Company com )
        {
            int id = com.Id;
            _service.DeleteCompany(id);
            Companies.Remove(com);
            
        }

    }
}
