using Utils;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace ViewModels
{
    class UpdateInvoiceViewModel : ModelViewBase
    {
        private IInvoiceService _service;

        private BankAccount _selectedBank;

        public BankAccount SelectedBank
        {
            get { return _selectedBank; }
            set
            {
                if (_selectedBank != value)
                {
                    _selectedBank = Banks.Find(b => b.Id == value.Id);
                    Invoice.BankId = _selectedBank.Id;
                }
            }
        }

        public InvoiceFacade Invoice { get; set; }
        public List<BankAccount> Banks { get; set; }

        public UpdateInvoiceViewModel(IInvoiceService iser, Invoice invoice)
        {
            _service = iser;

            UpdateInvoiceCommand = new RelayCommand(updateInvoice);
            CancelCommand = new RelayCommand(cancel);
            Invoice = new InvoiceFacade(invoice);
            Banks = _service.GetAllBankAccounts();

            SelectedBank = Banks.Find(b => b.Id == invoice.BankId);

        }

        public RelayCommand UpdateInvoiceCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private void updateInvoice()
        {
            _service.UpdateInvoice(Invoice.Invoice);
            Mediator.Notify("GoToView", "InvoiceView");
        }

        private void cancel()
        {
            Mediator.Notify("GoToView", "InvoiceView");
        }
    }
}
