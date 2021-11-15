using Models;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Utils;

namespace ViewModels
{
    public class CreateSoaViewModel : BindableBase,ModelViewBase
    {
        private ISoaService _service;
        private ICompanyService _companyService;

        private Company _selectedCompany;
        private BankAccount _selectedBank;
        private SoaElement _newElement;
        private SoaFacade _soa;
        
        public SoaFacade Soa
        {
            get { return _soa; }
            set
            {
                if(_soa != value)
                {
                    _soa = value;
                    OnPropertyChanged(nameof(Soa));
                }
            }
        }

        public double Total
        {
            get
            {
                double total = 0;
                if(SelectedElements != null)
                {
                    foreach(var element in SelectedElements)
                    {
                        total += element.Price;
                    }
                }

                return total;
            }
        }

        public ObservableCollection<SoaElement> Elements { get; set;}
        public ObservableCollection<SoaElement> SelectedElements { get; set; }
        
        public ObservableCollection<Company> Companies { get; set; }
        public List<BankAccount> Banks { get; set; }
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                if(value != _selectedCompany)
                {
                    _selectedCompany = value;
                    OnPropertyChanged(nameof(SelectedCompany));
                    SelectedBank = Banks.Find(b => b.Id == _selectedCompany.BankId);
                    SearchForItems();
                }
            }
        }
        public BankAccount SelectedBank
        {
            get { return _selectedBank; }
            set
            {
                if(value != _selectedBank)
                {
                    _selectedBank = value;
                    Soa.BankId = _selectedBank.Id;
                    OnPropertyChanged(nameof(SelectedBank));
                }
            }
        }

        public SoaElement NewElement
        {
            get { return _newElement; }
            set
            {
                if(_newElement != value)
                {
                    _newElement = value;
                    OnPropertyChanged(nameof(NewElement));
                }
            }
        }

        public CreateSoaViewModel(ISoaService service,ICompanyService comSer)
        {
            _service = service;
            _companyService = comSer;

            Soa = new SoaFacade();
            Soa.SoaDate = DateTime.Now;
            Soa.SoaNo = "#02" + Soa.SoaDate.Month + Soa.SoaDate.Year;
            Elements = new ObservableCollection<SoaElement>();
            SelectedElements = new ObservableCollection<SoaElement>();
            Companies = new ObservableCollection<Company>();
            Banks = _companyService.GetAllBankAccounts();

            foreach(var com in _companyService.GetAllCompanies())
            {
                Companies.Add(com);
            }

            SelectItemCommand = new RelayCommand<SoaElement>(selectItem);
            RemoveItemCommand = new RelayCommand<SoaElement>(removeItem);
            SaveCommand = new RelayCommand(save);
            CancelCommand = new RelayCommand(cancel);
            AddItemCommand = new RelayCommand(addNewElement);
        }

        public RelayCommand<SoaElement> SelectItemCommand { get; set; }
        public RelayCommand<SoaElement> RemoveItemCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }

        private void SearchForItems()
        {
            Elements.Clear();
            SelectedElements.Clear();
            NewElement = new SoaElement();
            var elements = _service.GetElementsByCompany(SelectedCompany.Id);

            foreach(var item in elements)
            {
                Elements.Add(item);
            }

        }

        private void selectItem(SoaElement element)
        {
            if(Elements != null)
            {
                if (Elements.Contains(element))
                {
                    Elements.Remove(element);
                    SelectedElements.Add(element);
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        private void removeItem(SoaElement element)
        {
           Elements.Add(element);
           SelectedElements.Remove(element);

            OnPropertyChanged(nameof(Total));
        }

        private void save()
        {
            if (SelectedElements.Count == 0)
            {
                MessageBox.Show("Please select one item at least");
                return;
            }

            Soa.SoaElements = new List<SoaElement>();
            foreach(var item in SelectedElements)
            {
                if (item.Invoices == null || item.Invoices.Count == 0)
                {
                    item.IsActive = false;
                    Soa.SoaElements.Add(new SoaElement()
                    {
                        Name = item.Name,
                        Price = item.Price,
                        IsActive = true,
                        IsPaid = false,
                        CompanyId = item.CompanyId
                    });
                }
                else
                {
                    item.IsActive = false;
                    Soa.SoaElements.Add(item);    
                }
                
            }

            Soa.CompanyId = SelectedCompany.Id;

            _service.CreateSoa(Soa.Soa);

            Mediator.Notify("GoToView", "SoaView");
               
        }

        private void addNewElement()
        {
            if(SelectedCompany == null)
            {
                MessageBox.Show("Please select company first.");
                return;
            }

            NewElement.CompanyId = SelectedCompany.Id;

            SelectedElements.Add(NewElement);
            NewElement = new SoaElement();

            OnPropertyChanged(nameof(Total));
        }

        private void cancel()
        {
            Mediator.Notify("GoToView", "SoaView");
        }


    }
}
