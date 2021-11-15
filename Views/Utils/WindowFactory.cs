using MimeKit;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;
using Views;

namespace Utils
{
    public class WindowFactory : IWindowFactory
    {
        private static SendEmailViewModel sendEmailViewModel;

        public static bool IsSendEmailViewModelInit { get; set; } = false;

        public void CreateEmailWindow(List<Company> companies, List<TempFile> files)
        {
            if(IsSendEmailViewModelInit == false)
            {
                IsSendEmailViewModelInit = true;
                sendEmailViewModel = new SendEmailViewModel(companies, files);
                SendEmaillView view = new SendEmaillView()
                {
                    DataContext = sendEmailViewModel
                };

                view.Show();
            }
            else
            {
                sendEmailViewModel.AddFiles(files);
            }

            

        }


    }
}
