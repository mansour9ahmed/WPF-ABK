using Microsoft.WindowsAPICodePack.Dialogs;
using Models;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Utils;

namespace ViewModels
{
    public class SoaViewModel : BindableBase,ModelViewBase
    {
        private ISoaService _service;

        private bool _isPaidFilter = false;
        private bool _isNotPaidFilter = false;
        private SoaFacade _selectedSoa;
        private IWindowFactory _windowFactory;

        public ObservableCollection<SoaFacade> Soas { get; set; }

        public SoaFacade SelectedSoa
        {
            get { return _selectedSoa; }
            set
            {
                if (_selectedSoa != value)
                {
                    _selectedSoa = value;
                    OnPropertyChanged(nameof(SelectedSoa));
                    DeleteCommand.RaiseCanExcuteChange();
                    SetPaidCommand.RaiseCanExcuteChange();
                    SaveAsPdfCommand.RaiseCanExcuteChange();
                    SaveAsExcelCommand.RaiseCanExcuteChange();
                    GotoSendEmailCommand.RaiseCanExcuteChange();
                }
            }
        }

        public List<Company> Companies { get; set; }
        public Company SelectedComapny { get; set; }
        public DateTime DateFilter { get; set; } = DateTime.Now;
        public bool IsPaidFilter
        {
            get { return _isPaidFilter; }
            set
            {
                if(_isPaidFilter != value)
                {
                    _isPaidFilter = value;
                    OnPropertyChanged(nameof(IsPaidFilter));
                    if(value && IsNotPaidFilter)
                    {
                        IsNotPaidFilter = false;
                    }
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
                    {
                        IsPaidFilter = false;
                    }
                }
            }
        }


        public SoaViewModel(ISoaService service, ICompanyService comService, IWindowFactory windowFactory)
        {
            _service = service;
            _windowFactory = windowFactory;

            Soas = new ObservableCollection<SoaFacade>();

            Companies = comService.GetAllCompanies();

            SearchCommand = new RelayCommand(search);
            GotoCreateSoaCommand = new RelayCommand(gotoCreateSoa);
            SaveAsPdfCommand = new RelayCommand(savePdf, () => SelectedSoa != null);
            SaveAsExcelCommand = new RelayCommand(saveExcel, () => SelectedSoa != null);
            SetPaidCommand = new RelayCommand(setPaid, () => SelectedSoa != null);
            DeleteCommand = new RelayCommand(deleteSoas, () => SelectedSoa != null);
            GotoSendEmailCommand = new RelayCommand(gotoSendEmail, () => SelectedSoa != null);
        }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand GotoCreateSoaCommand { get; set; }
        public RelayCommand SaveAsPdfCommand { get; set; }
        public RelayCommand SaveAsExcelCommand { get; set; }
        public RelayCommand SetPaidCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand GotoSendEmailCommand { get; set; }

        private void search()
        {
            Soas.Clear();

            bool? paid = null;
            if(IsPaidFilter == true && IsNotPaidFilter == false)
            {
                paid = true;
            }
            else if(IsPaidFilter == false & IsNotPaidFilter == true)
            {
                paid = false;
            }

            var soas = _service.SearchForSoa(SelectedComapny?.Id, DateFilter, paid);
            foreach(var soa in soas)
            {
                Soas.Add(new SoaFacade(soa));
            }
        }

        private void savePdf()
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
                foreach(var soa in Soas.Where(s => s.IsSelected).ToList())
                {
                    PdfReport.SaveSoaAsPdf(soa.Soa, dlg.FileName);
                }
                MessageBox.Show("Saved Successfully");
            }

        }

        private void saveExcel()
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
                foreach (var soa in Soas.Where(s => s.IsSelected).ToList())
                {
                    PdfReport.SaveSoaAsExcel(soa.Soa, dlg.FileName);
                }
                MessageBox.Show("Saved Successfully");
            }

        }

        private void setPaid()
        {
            foreach (var soa in Soas.Where(s => s.IsSelected).ToList())
            {
                _service.SetPaid(soa.Id);
            }
            search();
        }

        private void deleteSoas()
        {
            List<SoaFacade> deletedItems = Soas.Where(s => s.IsSelected).ToList();
            foreach (var soa in deletedItems)
            {
                _service.DeleteSoa(soa.Id);
                Soas.Remove(soa);
            }
        }
        private void gotoSendEmail()
        {
            var e = EmailSender.CreateSoaFiles(Soas.Where(i => i.IsSelected).Select(i => i.Soa).ToList());

            _windowFactory.CreateEmailWindow(Companies, e);
        }
        private void gotoCreateSoa()
        {
            Mediator.Notify("GoToView", "CreateSoaView");
        }
    }
}
