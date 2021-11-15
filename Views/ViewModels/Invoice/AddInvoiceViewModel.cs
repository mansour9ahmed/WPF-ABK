using Utils;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
namespace ViewModels
{
    class AddInvoiceViewModel :BindableBase, ModelViewBase
    {
        private IInvoiceService _service;
        private IVesselService _vesselService;
        public InvoiceFacade Invoice { get; set; }

        private Vessel _selectedVessel;
        private BankAccount _selectedBank;

        
        public List<BankAccount> Banks { get; set; }
        public Vessel SelectedVessel
        {
            get { return _selectedVessel; }
            set
            {
                if(_selectedVessel != value)
                {
                    _selectedVessel = value;
                    Invoice.VesselId = _selectedVessel.Id;
                    Invoice.OverMbPrice = _selectedVessel.OverMbPrice;
                    Invoice.OverMinPrice = _selectedVessel.OverMinPrice;
                    Invoice.IntegraPrice = _selectedVessel.IntegraPrice;
                    Invoice.BundleName = _selectedVessel.BundleName;
                    Invoice.BundlePrice = _selectedVessel.BundlePrice;
                    SelectedBank = _selectedVessel.Company?.Bank;
                }
            }
        }

        public BankAccount SelectedBank
        {
            get { return _selectedBank; }
            set
            {
                if(_selectedBank != value)
                {
                    _selectedBank = Banks.Find(b => b.Id == value.Id);
                    Invoice.BankId = _selectedBank.Id;
                    OnPropertyChanged(nameof(SelectedBank));
                }
            }
        }

        public List<Vessel> Vessels { get; set; }

        public AddInvoiceViewModel(IInvoiceService iser,IVesselService vser)
        {
            _service = iser;
            _vesselService = vser;

            AddInvoiceCommand = new RelayCommand(addInvoice);
            CancelCommand = new RelayCommand(cancel);
            Invoice = new InvoiceFacade();
            Invoice.Date = DateTime.Now;
            Invoice.InvoiceNo = "#01" + Invoice.Date.Month.ToString()+ Invoice.Date.Year.ToString(); 

            Vessels = _vesselService.GetAllVessels();
            Banks = _service.GetAllBankAccounts();
           
        }

        public RelayCommand AddInvoiceCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private void addInvoice()
        {
            _service.CreateInvoice(Invoice.Invoice);
            Mediator.Notify("GoToView", "InvoiceView");
        }

        private void cancel()
        {
            Mediator.Notify("GoToView", "InvoiceView");
        }
    }
}
