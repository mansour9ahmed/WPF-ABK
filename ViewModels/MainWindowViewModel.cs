using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;

        private CompanyViewModel _companyViewModel;


        public MainWindowViewModel(ICompanyService comService)
        {
            _companyViewModel = new CompanyViewModel(comService);
            NavCommand = new RelayCommand<string>(OnNav);


            _currentViewModel = _companyViewModel;
        }
        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetValue(ref _currentViewModel, value); }
        }


        public RelayCommand<string> NavCommand { get; set; }
    

        private void OnNav(string dest)
        {
            switch(dest)
            {
                case "CompanyView":
                    CurrentViewModel = _companyViewModel;
                    break;
            }
        }


    }
}
