using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows;
using Utils;

namespace ViewModels
{
    public class SettingsViewModel: BindableBase,ModelViewBase
    {
        private string _host;
        private int _port;
        private string _email;
        private string _password;
        private string _appPassword;
        public string Host
        {
            get { return _host; }
            set
            {
                if(value != _host)
                {
                    _host = value;
                    OnPropertyChanged(nameof(Host));
                }
            }
        }

        public int Port
        {
            get { return _port; }
            set
            {
                if(value != _port)
                {
                    _port = value;
                    OnPropertyChanged(nameof(Port));
                }
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if(value != _password)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public String AppPassword
        {
            get { return _appPassword; }
            set
            {
                if(_appPassword != value)
                {
                    _appPassword = value;
                    OnPropertyChanged(nameof(AppPassword));
                }
            }
        }

        public SettingsViewModel()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Host = config.AppSettings.Settings["Host"].Value;
            Port = int.Parse(config.AppSettings.Settings["Port"].Value);
            Email = config.AppSettings.Settings["Email"].Value;
            Password = config.AppSettings.Settings["Password"].Value;
            AppPassword = config.AppSettings.Settings["AppPassword"].Value;

            SaveCommand = new RelayCommand(save);
            TestConnectionCommand = new RelayCommand(testConnection);
            ChangePasswordCommand = new RelayCommand(changePassword);
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand TestConnectionCommand { get; set; }

        public RelayCommand ChangePasswordCommand { get; set; }

        private void save()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["Host"].Value = Host;
            config.AppSettings.Settings["Port"].Value = Port.ToString();
            config.AppSettings.Settings["Email"].Value = Email;
            config.AppSettings.Settings["Password"].Value = Password;
            config.Save(ConfigurationSaveMode.Modified);
            MessageBox.Show("Configurations saved.");
        }

        private void testConnection()
        {
            EmailSender.TestConnecton(Host, Port, Email, Password);
            MessageBox.Show("Connected successfully");
        }

        private void changePassword()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


            config.AppSettings.Settings["AppPassword"].Value = AppPassword;
            config.Save(ConfigurationSaveMode.Modified);
            MessageBox.Show("Password Changed.");
        }

    }
}
