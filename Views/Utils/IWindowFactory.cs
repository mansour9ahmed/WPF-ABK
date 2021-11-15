using MimeKit;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public interface IWindowFactory
    {
    //    public void CreateEmailWindow(List<Company> companies, AttachmentCollection attachments);
        public void CreateEmailWindow(List<Company> companies, List<TempFile> attachments);
    }
}
