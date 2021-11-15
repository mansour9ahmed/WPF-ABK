using Utils;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Views;
using Services;
using Services.Services;
using System.Configuration;

namespace ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
       
        private ModelViewBase _CurrentViewModel;
        private CompanyService _companyService;
        private VesselService _vesselService;
        private InvoiceService _invoiceService;
        private SoaService _soaService;
        private TransactionService _transactionService;

        public ModelViewBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        public MainWindowViewModel()
        {

            string sqlString = ConfigurationManager.ConnectionStrings["BurakMarineDb"].ConnectionString;
            BurakDbContext context = new ContextFactory().CreateDbContext(new string[] { sqlString });

            _companyService = new CompanyService(context);
            _vesselService = new VesselService(context);
            _invoiceService = new InvoiceService(context);
            _soaService = new SoaService(context);
            _transactionService = new TransactionService(context);

            GoToView("InvoiceView");
            GotoCommand = new RelayCommand<string>(GoToView);

            Mediator.Subscribe("GoToView", GoToView);
        }

        private void GoToView(string viewName,object obj)
        {
            WindowFactory windowFactory = new WindowFactory();

            switch (viewName)
            {
                case "CompanyView":
                    CurrentViewModel = new CompanyViewModel(_companyService);
                    break;
                case "AddCompanyView":
                    CurrentViewModel = new AddCompanyViewModel(_companyService);
                    break;
                case "UpdateCompanyView":
                    Company company = (Company)obj;
                    CurrentViewModel = new UpdateCompanyViewModel(_companyService, company);
                    break;
                case "VesselView":
                    CurrentViewModel = new VesselViewModel(_vesselService);
                    break;
                case "AddVesselView":
                    CurrentViewModel = new AddVesselViewModel(_vesselService, _companyService);
                    break;
                case "UpdateVesselView":
                    CurrentViewModel = new UpdateVesselViewModel(_vesselService, _companyService, (Vessel)obj);
                    break;
                case "InvoiceView":
                    CurrentViewModel = new InvoiceViewModel(_invoiceService, _companyService, windowFactory);
                    break;
                case "AddInvoiceView":
                    CurrentViewModel = new AddInvoiceViewModel(_invoiceService, _vesselService);
                    break;
                case "UpdateInvoiceView":
                    Invoice invoice = (Invoice)obj;
                    CurrentViewModel = new UpdateInvoiceViewModel(_invoiceService, invoice);
                    break;
                case "SoaView":
                    CurrentViewModel = new SoaViewModel(_soaService, _companyService, windowFactory);
                    break;
                case "CreateSoaView":
                    CurrentViewModel = new CreateSoaViewModel(_soaService, _companyService);
                    break;
                case "ItemsView":
                    CurrentViewModel = new ItemsViewModel(_soaService, _companyService);
                    break;
                case "TransactionAccountView":
                    CurrentViewModel = new TransactionAccountViewModel(_transactionService);
                    break;
                case "TransactionsView":
                    int accountId = (int)obj;
                    CurrentViewModel = new TransactionsViewModel(_transactionService,accountId);
                    break;
                case "SettingsView":
                    CurrentViewModel = new SettingsViewModel();
                    break;
            }
        }

        private void GoToView(string viewName)
        {
            GoToView(viewName, null);
        }

        

        public RelayCommand<string> GotoCommand { get; set; }


    }
}
