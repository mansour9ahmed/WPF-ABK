using MimeKit;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Utils;

namespace ViewModels
{
    public class SendEmailViewModel : BindableBase, ModelViewBase
    {
        private List<string> _cc = new List<string>();
        private List<string> _to = new List<string>();
        private string _body;
        private Company _company;
        private string _subject;
        private AttachmentCollection _attachments;
        private List<TempFile> _files;
        public List<string> To
        {
            get
            {
                return _to;
            }
            set
            {
                if (value != _to)
                {
                    _to = value;
                    OnPropertyChanged("To");
                }
            }
        }
        public List<string> CC
        {
            get { return _cc; }
            set
            {
                if (_cc != value)
                {
                    _cc = value;
                    OnPropertyChanged("CC");
                }
            }
        }
        public string Subject
        {
            get { return _subject; }
            set
            {
                if (value != _subject)
                {
                    _subject = value;
                    OnPropertyChanged("Subject");
                }
            }
        }
        public string Body
        {
            get { return _body; }
            set
            {
                if (value != _body)
                {
                    _body = value;
                    OnPropertyChanged("Body");
                }
            }
        }

        public ObservableCollection<KeyValuePair<string,string>> FilesName
        {
            get; set;
        }
        public ObservableCollection<Company> Companies { get; set; }
        public Company SelectedCompany
        {
            get { return _company; }
            set
            {
                if (value != _company)
                {
                    _company = value;
                    OnPropertyChanged("SelectedCompany");
                    To = _company.Emails.Select(c => c.Email).ToList();
                }
            }
        }
        public ObservableCollection<TempFile> Files { get; set; }

        public SendEmailViewModel(List<Company> companies, AttachmentCollection attachments)
        {
            _attachments = attachments;
            Companies = new ObservableCollection<Company>();
            foreach (var company in companies)
            {
                Companies.Add(company);
            }

            FilesName = new ObservableCollection<KeyValuePair<string, string>>();

            if (attachments != null)
            {

                foreach (var item in attachments)
                {
                    if (item is MimePart)
                    {
                        FilesName.Add(new KeyValuePair<string, string>(((MimePart)item).FileName,""));
                    }
                }
            }

            CC = new List<string>();
            CC.Add("captaamer81@gmail.com");
            CC.Add("ops@alburakmarine.com");

            Subject = "AIRTIME INVOICE - " + DateTime.Now.ToString("MMM - yyyy");
            Body = "Hello,\n Please find the attached files \n Regards. ";

            SendEmailCommand = new RelayCommand(sendEmail);
            CancelCommand = new RelayCommand<Window>(cancel);

        }

        public SendEmailViewModel(List<Company> companies, List<TempFile> files)
        {
            _files = files;
            _attachments = new AttachmentCollection();
            Companies = new ObservableCollection<Company>();
            foreach (var company in companies)
            {
                Companies.Add(company);
            }

            FilesName = new ObservableCollection<KeyValuePair<string, string>>();

            if (files != null)
            {
                var bodyBuilder = new BodyBuilder();

                foreach (var item in files)
                {
                    string fileName = item.Path.Substring(item.Path.LastIndexOf("\\") + 1);
                    FilesName.Add(new KeyValuePair<string, string>(fileName,item.Path));
                    byte[] bytes = File.ReadAllBytes(item.Path);
                    bodyBuilder.Attachments.Add(fileName, bytes, ContentType.Parse("Application/pdf"));
                }

                _attachments = bodyBuilder.Attachments;
            }

            CC = new List<string>();
            CC.Add("captaamer81@gmail.com");
            CC.Add("ops@alburakmarine.com");

            Subject = "AIRTIME INVOICE - " + DateTime.Now.ToString("MMM - yyyy");
            Body = "Hello,\n Please find the attached files \n Regards. ";

            SendEmailCommand = new RelayCommand(sendEmail);
            CancelCommand = new RelayCommand<Window>(cancel);
            OpenFileCommand = new RelayCommand<string>(openFile);

        }

        public RelayCommand SendEmailCommand { get; set; }
        public RelayCommand<Window> CancelCommand { get; set; }
        public RelayCommand<string> OpenFileCommand { get; set; }

        private void sendEmail()
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("tech@alburakmarine.com"));

            if(To.Count == 0)
            {
                throw new Exception("please add revicever email");
            }

            foreach(var item in To)
            {
                message.To.Add(MailboxAddress.Parse(item));
            }

            if(message.Cc != null && message.Cc.Count != 0)
            {
                foreach (var item in CC)
                {
                    if (item.Contains("@") && item.Length > 3) 
                        message.Cc.Add(MailboxAddress.Parse(item));
                }
            }
            

            message.Subject = Subject;

            BodyBuilder builder = new BodyBuilder();
            builder.TextBody = Body;
            
            if(_files != null)
            {
                foreach (var file in _files)
                {
                    string fileName = file.Path.Substring(file.Path.LastIndexOf("\\") + 1);
                    byte[] bytes = File.ReadAllBytes(file.Path);
                    builder.Attachments.Add(fileName, bytes, ContentType.Parse("Application/pdf"));
                }
            }
            

            message.Body = builder.ToMessageBody();

            EmailSender.SendEmail(message);
            MessageBox.Show("sent");
        }

        private void openFile(string file)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });
        }

        private void cancel(Window window)
        {
            if (_files != null)
            {
                foreach (var file in _files)
                {
                    file.Dispose();
                }
            }

            WindowFactory.IsSendEmailViewModelInit = false;

            if (window != null)
            {
                window.Close();
            }
        }

        public void AddFiles(List<TempFile> files)
        {
            if(_files == null)
            {
                _files = new List<TempFile>();
            }


            _files.AddRange(files);

            if (files != null)
            {

                foreach (var item in files)
                {
                    string fileName = item.Path.Substring(item.Path.LastIndexOf("\\") + 1);
                    FilesName.Add(new KeyValuePair<string, string>(fileName, item.Path));
                }

                
            }
        }
    }
}
