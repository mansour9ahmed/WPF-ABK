using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModels;

namespace Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string password = passwordTb.Password;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string pas = config.AppSettings.Settings["AppPassword"].Value;

            if(pas == password)
            {
                MainWindowView mainWindow = new MainWindowView();

                MainWindowViewModel context = new MainWindowViewModel();
                mainWindow.DataContext = context;

                mainWindow.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Wrong Password!!!!!");
            }

        }
    }
}
