using Microsoft.WindowsAPICodePack.Dialogs;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Utils;

namespace ViewModels
{

    public class TransactionsViewModel : BindableBase, ModelViewBase
    {
        private ITransactionService _service;

        private DateTime _newTransactionDate;
        private double _newTransactionAmount;
        private TransactionStatus _newTransactionStatus;
        private BankAccount _selectedBank;
        private string _newTransactionNote;


        public TransactionAccount Account { get; set; }

        public ObservableCollection<Transaction> Transactions { get; set; }
        public List<BankAccount> Banks { get; set; }
        public BankAccount SelectedBank
        {
            get { return _selectedBank; }
            set 
            {
                if(_selectedBank != value)
                {
                    _selectedBank = Banks.Find(b => b.Id == value.Id);
                    OnPropertyChanged(nameof(SelectedBank));
                }
            }
        }

        

        public double Total
        {
            get
            {
                double total = 0;
                if(Transactions != null)
                {
                    foreach(var item in Transactions)
                    {
                        if (item.IsInput)
                            total += item.Amount;
                        else
                            total -= item.Amount;
                    }
                }

                return total;
            }
        }

        public string NewTransactionNote
        {
            get { return _newTransactionNote; }
            set
            {
                if(_newTransactionNote != value)
                {
                    _newTransactionNote = value;
                    OnPropertyChanged(nameof(NewTransactionNote));
                }
            }
        }

        public List<TransactionStatus> Statuses { get; } = new List<TransactionStatus>()
        {
            new TransactionStatus(){Name = "Recieved", IsInput = true},
            new TransactionStatus(){Name = "Paid", IsInput = false}
        };
        public TransactionStatus SelectedStatus
        {
            get { return _newTransactionStatus; }
            set
            {
                if(_newTransactionStatus != value)
                {
                    _newTransactionStatus = Statuses.Find(s => s.IsInput == value.IsInput);
                    OnPropertyChanged(nameof(SelectedStatus));
                }
            }
        }
        public DateTime NewTransactionDate
        {
            get { return _newTransactionDate; }
            set
            {
                if(_newTransactionDate != value)
                {
                    _newTransactionDate = value;
                    OnPropertyChanged(nameof(NewTransactionDate));
                }
            }
        }
        public double NewTransactionAmount
        {
            get { return _newTransactionAmount; }
            set
            {
                if(_newTransactionAmount != value)
                {
                    _newTransactionAmount = value;
                    OnPropertyChanged(nameof(NewTransactionAmount));
                }
            }
        }

        public TransactionsViewModel(ITransactionService service, int AccountId)
        {
            _service = service;

            Account = service.GetAccount(AccountId);

            Banks = new List<BankAccount>();
            Banks.Add(new BankAccount() { Id = 0, Name = "Cash" });
            Banks.AddRange(_service.GetAllBankAccounts());

            SelectedBank = Banks[0];
            NewTransactionDate = DateTime.Now;
            SelectedStatus = Statuses[0];


            Transactions = new ObservableCollection<Transaction>();
            if(Account.Transactions == null)
            {
                Account.Transactions = new List<Transaction>();
            }
            foreach(var item in Account.Transactions.OrderBy(t => t.IsInput))
            {
                if(item.BankId == null)
                {
                    item.Bank = new BankAccount();
                    item.Bank.Name = "Cash";
                }
                Transactions.Add(item);
            }

            AddNewTransactionCommand = new RelayCommand(addNewTransaction);
            DeleteTransactionCommand = new RelayCommand<Transaction>(deleteTransaction);
            CancelCommand = new RelayCommand(cancel);
            SaveAsPdfCommand = new RelayCommand(saveAsPdf);
        }

        public RelayCommand AddNewTransactionCommand { get; set; }
        public RelayCommand<Transaction> DeleteTransactionCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveAsPdfCommand { get; set; }

        private void addNewTransaction()
        {
            if(SelectedStatus == null)
            {
                MessageBox.Show("Please select transaction status");
                return;
            }

            Transaction transaction = new Transaction();
            transaction.AccountId = Account.Id;
            transaction.Date = NewTransactionDate;
            transaction.Amount = NewTransactionAmount;
            transaction.IsInput = SelectedStatus.IsInput;
            transaction.Note = NewTransactionNote;

            if(SelectedBank.Id != 0)
                transaction.BankId = SelectedBank.Id;

            var tra = _service.CreateTransaction(transaction);

            if(tra.BankId == null)
            {
                tra.Bank = new BankAccount();
                tra.Bank.Name = "Cash";
            }
            Transactions.Add(tra);
            Account.Transactions.Add(tra);

            OnPropertyChanged(nameof(Total));

            NewTransactionDate = DateTime.Now;
            NewTransactionAmount = 0.0;
            NewTransactionNote = "";


        }

        private void deleteTransaction(Transaction transaction)
        {
            _service.DeleteTransaction(transaction.Id);
            Transactions.Remove(transaction);
            OnPropertyChanged(nameof(Total));
        }

        private void cancel()
        {
            Mediator.Notify("GoToView", "TransactionAccountView");
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
                PdfReport.SaveTransactionAccountAsPdf(Account, dlg.FileName);
                MessageBox.Show("Saved Successfully");
            }

        }

    }

    public class TransactionStatus
    {
        public string Name { get; set; }
        public bool IsInput { get; set; }
    }
}
