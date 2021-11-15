using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using Utils;

namespace ViewModels
{
    public class TransactionAccountViewModel : BindableBase, ModelViewBase
    {
        private ITransactionService _service;
        private TransactionAccount _selectedAccount;
        private string _newAccountName;
        public ObservableCollection<TransactionAccount> Accounts { get; set; }
        public TransactionAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                if (_selectedAccount != value)
                {
                    _selectedAccount = value;
                    OnPropertyChanged(nameof(SelectedAccount));
                }
            }
        }

        public string NewAccountName
        {
            get { return _newAccountName; }
            set
            {
                if(value != _newAccountName)
                {
                    _newAccountName = value;
                    OnPropertyChanged(nameof(NewAccountName));
                }
            }
        }

        public TransactionAccountViewModel(ITransactionService service)
        {
            _service = service;

            Accounts = new ObservableCollection<TransactionAccount>();

            foreach(var account in _service.GetAllAccounts())
            {
                Accounts.Add(account);
            }

            DeleteAccountCommand = new RelayCommand<TransactionAccount>(deleteAccount);
            AddAccountCommand = new RelayCommand(addAccount);
            GotoAccountDetalisCommand = new RelayCommand<TransactionAccount>(gotoAccountDetails);
        }

        public RelayCommand AddAccountCommand { get; set; }
        public RelayCommand<TransactionAccount> GotoAccountDetalisCommand { get; set; }
        public RelayCommand<TransactionAccount> DeleteAccountCommand { get; set; }
        public RelayCommand<TransactionAccount> GotoUpdateAccountCommand { get; set; }

        private void deleteAccount(TransactionAccount account)
        {
            _service.DeleteAccount(account.Id);
            Accounts.Remove(account);
        }

        private void addAccount()
        {
            if(NewAccountName == null || NewAccountName == "")
            {
                MessageBox.Show("Please enter account name");
                return;
            }

            TransactionAccount newAccount = new TransactionAccount()
            {
                Name = NewAccountName
            };
            var AddedAccount = _service.CreateAccount(newAccount);

            Accounts.Add(AddedAccount);
            NewAccountName = "";
        }

        private void gotoAccountDetails(TransactionAccount account)
        {
            Mediator.Notify("GoToView", "TransactionsView", account.Id);
        }
    }
}
