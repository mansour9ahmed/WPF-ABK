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
    public class ItemsViewModel : BindableBase,ModelViewBase
    {
        private ISoaService _service;
        private SoaElement _newElement;
        private Company _selectedCompany;
        private bool _isPaidFilter = false;
        private bool _isNotPaidFilter = false;

        public ObservableCollection<SoaElement> Elements { get; set; }
        public ObservableCollection<Company> Companies { get; set; }
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                if (value != _selectedCompany)
                {
                    _selectedCompany = value;
                    OnPropertyChanged(nameof(SelectedCompany));
                    SearchForItems();
                }
            }
        }

        public SoaElement NewElement
        {
            get { return _newElement; }
            set
            {
                if (_newElement != value)
                {
                    _newElement = value;
                    OnPropertyChanged(nameof(NewElement));
                }
            }
        }

        public bool IsPaidFilter
        {
            get { return _isPaidFilter; }
            set
            {
                if(_isPaidFilter != value)
                {
                    _isPaidFilter = value;
                    if(value && IsNotPaidFilter)
                    {
                        IsNotPaidFilter = false;
                    }
                    SearchForItems();
                    OnPropertyChanged(nameof(IsPaidFilter));
                }
            }
        }

        public bool IsNotPaidFilter
        {
            get { return _isNotPaidFilter; }
            set
            {
                if(_isNotPaidFilter != value)
                {
                    _isNotPaidFilter = value;
                    if(value && IsPaidFilter)
                    {
                        IsPaidFilter = false;
                    }
                    SearchForItems();
                    OnPropertyChanged(nameof(IsNotPaidFilter));
                }
            }
        }

        public double Total
        {
            get
            {
                double total = 0;
                if(Elements != null)
                {
                    foreach(var element in Elements)
                    {
                        total += element.Price;
                    }
                }

                return total;
            }
        }

        public ItemsViewModel(ISoaService service,ICompanyService comSer)
        {
            _service = service;
            Elements = new ObservableCollection<SoaElement>();

            Companies = new ObservableCollection<Company>();
            foreach (var com in comSer.GetAllCompanies())
            {
                Companies.Add(com);
            }

            NewElement = new SoaElement();
            NewElement.Price = 0;

            AddItemCommand = new RelayCommand(addItem);
            RemoveItemCommand = new RelayCommand<SoaElement>(deleteItem);
        }

        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand<SoaElement> RemoveItemCommand { get; set; }

        private void SearchForItems()
        {
            Elements.Clear();

            bool? paid = null;

            if(IsPaidFilter == true && IsNotPaidFilter == false)
            {
                paid = true;
            }
            else if (IsPaidFilter == false && IsNotPaidFilter == true)
            {
                paid = false;
            }

            var elements = _service.GetPurchasesByCompany(SelectedCompany.Id, paid);

            foreach (var item in elements)
            {
                Elements.Add(item);
            }

            OnPropertyChanged(nameof(Total));
        }

        private void addItem()
        {
            if(SelectedCompany == null)
            {
                MessageBox.Show("Please select comapny first");
                return;
            }

            if(NewElement.Name == null || NewElement.Name == "")
            {
                MessageBox.Show("Please add description first");
                return;
            }

            NewElement.CompanyId = SelectedCompany.Id;
            NewElement.IsActive = true;

            var item = _service.CreateSoaElement(NewElement);
            Elements.Add(item);
            NewElement = new SoaElement();
            NewElement.Price = 0;

            OnPropertyChanged(nameof(Total));

        }

        private void deleteItem(SoaElement element)
        {
            _service.DeleteSoaElement(element.Id);
            Elements.Remove(Elements.Where(e => e.Id == element.Id).FirstOrDefault());
            OnPropertyChanged(nameof(Total));
        }

    }
}
