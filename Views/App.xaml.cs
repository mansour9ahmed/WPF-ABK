using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Utils;
using ViewModels;

namespace Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string pas = config.AppSettings.Settings["AppPassword"].Value;

            if (pas == null || pas == "")
            {
                MainWindowView mainWindow = new MainWindowView();

                MainWindowViewModel context = new MainWindowViewModel();
                mainWindow.DataContext = context;

                mainWindow.Show();

            }
            else
            {
                LoginView app = new LoginView();
                app.Show();
            }

           
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }
    }

}
