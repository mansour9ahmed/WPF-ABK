using Utils;
using GemBox.Spreadsheet;
using Microsoft.WindowsAPICodePack.Dialogs;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace ViewModels
{
    public class InvoiceViewModel : BindableBase, ModelViewBase
    {
        private IInvoiceService _service;
        private ICompanyService _comService;
        private IWindowFactory _windowFactory;

        private InvoiceFacade _selectedInvoice;
        private bool _isPaidFilter;
        private bool _isNotPaidFilter;
        private Company _companyFilter;
        private DateTime _fromFilter;
        private DateTime _toFilter;
        public ObservableCollection<InvoiceFacade> Invoices { get; set; }

        public List<Company> Companies { get; set; }
        public ObservableCollection<Vessel> Vessels { get; set; }
        public InvoiceFacade SelectedInvoice
        {
            get { return _selectedInvoice; }
            set
            {
                if(_selectedInvoice != value)
                {
                    _selectedInvoice = value;
                    OnPropertyChanged(nameof(SelectedInvoice));
                    SetPaidCommand.RaiseCanExcuteChange();
                    DeleteCommand.RaiseCanExcuteChange();
                    GoToSendEmailCommand.RaiseCanExcuteChange();
                    SavePdfCommand.RaiseCanExcuteChange();
                }
            }
        }

        public int NoOfInvoices
        {
            get { return Invoices.Count; }
        }


        public double TotalInvoices
        {
            get
            {
                double total = 0;
                foreach (var invoice in Invoices)
                    total += invoice.Total;
                return total;
            }
        }

        public DateTime FromDateFilter
        {
            get { return _fromFilter; }
            set
            {
                if(value != _fromFilter)
                {
                    _fromFilter = value;
                    OnPropertyChanged(nameof(FromDateFilter));
                }
            }
        }
        public DateTime ToDateFilter
        {
            get { return _toFilter; }
            set
            {
                if(value != _toFilter)
                {
                    _toFilter = value;
                    OnPropertyChanged(nameof(ToDateFilter));
                }
            }
        }
        public Company CompanyFilter
        {
            get { return _companyFilter; }
            set
            {
                if(_companyFilter != value && value != null)
                {
                    _companyFilter = Companies.Find(c => c.Id == value.Id);
                    OnPropertyChanged(nameof(CompanyFilter));
                    Vessels.Clear();
                    Vessels.Add(new Vessel() { Id = 0, Name = "All Vessels" });
                    if(CompanyFilter.Vessels != null)
                    {
                        foreach (var ves in CompanyFilter.Vessels)
                            Vessels.Add(ves);
                    }
                }
            }
        }
        public Vessel VesselFilter { get; set; } = null;
        public bool IsPaidFilter
        {
            get { return _isPaidFilter; }
            set
            {
                if(_isPaidFilter != value)
                {
                    _isPaidFilter = value;
                    OnPropertyChanged(nameof(IsPaidFilter));
                    if (value && IsNotPaidFilter)
                        IsNotPaidFilter = false;
                }
            }
        }
        public bool IsNotPaidFilter
        {
            get { return _isNotPaidFilter; }
            set
            {
                if (_isNotPaidFilter != value)
                {
                    _isNotPaidFilter = value;
                    OnPropertyChanged(nameof(IsNotPaidFilter));
                    if (value && IsPaidFilter)
                        IsPaidFilter = false;
                }
            }
        }


        public InvoiceViewModel(IInvoiceService service, ICompanyService comSer,IWindowFactory windowFactory)
        {
            _service = service;
            _comService = comSer;
            _windowFactory = windowFactory;
            Invoices = new ObservableCollection<InvoiceFacade>();

            Companies = new List<Company>();
            Vessels = new ObservableCollection<Vessel>();

            Companies.Add(new Company() { Id = 0, Name = "All Companies" });
            //Vessels.Add(new Vessel() { Id = 0, Name = "All Vessels" });

            Companies.AddRange(_comService.GetAllCompanies());
            //Vessels.AddRange(_vesService.GetAllVessels());

            SearchCommand = new RelayCommand(search);
            GotoAddInvoiceCommand = new RelayCommand(gotoAddInvoice);
            GoToUpdateInvoiceCommand = new RelayCommand<InvoiceFacade>(gotoUpdateInvoice);
            SavePdfCommand = new RelayCommand(saveAsPdf, () => SelectedInvoice != null);
            GoToSendEmailCommand = new RelayCommand(gotoSendEmail, () => SelectedInvoice != null);
            SetPaidCommand = new RelayCommand(setPaid, () => SelectedInvoice != null);
            DeleteCommand = new RelayCommand(delete, () => SelectedInvoice != null);

            IsPaidFilter = InvoiceFilterValues.IsPaidFilter;
            IsNotPaidFilter = InvoiceFilterValues.IsNotPaidFilter;
            FromDateFilter = InvoiceFilterValues.FromDateFilter;
            ToDateFilter = InvoiceFilterValues.ToDateFilter;
            CompanyFilter = InvoiceFilterValues.CompanyFilter;
            search();

        }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand GotoAddInvoiceCommand { get; set; }
        public RelayCommand SavePdfCommand { get; set; }
        public RelayCommand<InvoiceFacade> GoToUpdateInvoiceCommand { get; set; }
        public RelayCommand GoToSendEmailCommand { get; set; }
        public RelayCommand SetPaidCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private void search()
        {
            Invoices.Clear();

            bool? paid = null;

            if (IsPaidFilter && !IsNotPaidFilter)
                paid = true;
            else if (!IsPaidFilter && IsNotPaidFilter)
                paid = false;

            foreach (var invoice in _service.SearchForInvoices(FromDateFilter,ToDateFilter, paid, VesselFilter?.Id, CompanyFilter?.Id))
            {
                Invoices.Add(new InvoiceFacade(invoice));
            }

            OnPropertyChanged(nameof(TotalInvoices));
            OnPropertyChanged(nameof(NoOfInvoices));

            InvoiceFilterValues.IsPaidFilter = IsPaidFilter;
            InvoiceFilterValues.IsNotPaidFilter = IsNotPaidFilter;
            InvoiceFilterValues.FromDateFilter = FromDateFilter;
            InvoiceFilterValues.ToDateFilter = ToDateFilter;
            InvoiceFilterValues.CompanyFilter = CompanyFilter;
        }

        private void gotoAddInvoice()
        {
            Mediator.Notify("GoToView", "AddInvoiceView");
        }
        private void gotoUpdateInvoice(InvoiceFacade invoice)
        {
            if(invoice.IsPaid)
            {
                throw new Exception("You cannot edit paid invoice.");
            }
            Mediator.Notify("GoToView", "UpdateInvoiceView", invoice.Invoice);
        }

        private void saveAsPdf()
        {
            

            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Select folder";
            dlg.IsFolderPicker = true;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach(var invoice in Invoices.Where(i => i.IsSelected))
                {
                    PdfReport.SaveInoiceAsPdf(invoice.Invoice, dlg.FileName);
                }
                MessageBox.Show("Saved Successfully");
            }

        }

        private void gotoSendEmail()
        {
            var e = EmailSender.CreateInvoiceFiles(Invoices.Where(i => i.IsSelected).Select(i => i.Invoice).ToList());

            _windowFactory.CreateEmailWindow(Companies, e);
        }

        private void setPaid()
        {
            foreach(var invoice in Invoices.Where(i => i.IsSelected && !i.IsPaid))
            {
                _service.SetPaid(invoice.Invoice.Id);
            }
            OnPropertyChanged(nameof(Invoices));

            search();
        }

        private void delete()
        {
            List<InvoiceFacade> deleted = new List<InvoiceFacade>();
            foreach (var invoice in Invoices.Where(i => i.IsSelected ))
            {
                bool b =_service.DeleteInvoice(invoice.Invoice.Id);
                if (b) deleted.Add(invoice);
            }

            foreach(var invoice in deleted)
            {
                Invoices.Remove(invoice);
            }

            OnPropertyChanged(nameof(NoOfInvoices));
            OnPropertyChanged(nameof(TotalInvoices));
        }


    }
}
