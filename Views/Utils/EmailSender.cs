using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Utils
{
    public static class EmailSender
    {
        //public static AttachmentCollection CreateInvoiceAttachments(List<Invoice> invoices)
        //{
        //    if(invoices == null || invoices.Count == 0)
        //    {
        //        return null;
        //    }
         
        //    var bodyBuilder = new BodyBuilder();

        //    foreach(var invoice in invoices)
        //    {
        //        using (var tmpFile = PdfReport.CreateInvoiceTempPdf(invoice))
        //        {
        //            byte[] bytes = File.ReadAllBytes(tmpFile.Path);
        //            string fileName = invoice.Vessel.Name + "_" + invoice.Date.ToString("MMMM") + "_" +DateTime.Now.Ticks + ".pdf";
        //            bodyBuilder.Attachments.Add(fileName, bytes, ContentType.Parse("Application/pdf"));
        //        }
        //    }

        //    return bodyBuilder.Attachments;
        //}

        public static List<TempFile> CreateInvoiceFiles(List<Invoice> invoices)
        {
            if (invoices == null || invoices.Count == 0)
            {
                return null;
            }

            List<TempFile> files = new List<TempFile>();


            foreach (var invoice in invoices)
            {
                var tmpFile = PdfReport.CreateInvoiceTempPdf(invoice);
                files.Add(tmpFile);
            }

            return files;
        }

        //public static AttachmentCollection CreateSoaAttachments(List<Soa> soas)
        //{
        //    if (soas == null || soas.Count == 0)
        //    {
        //        return null;
        //    }

        //    var bodyBuilder = new BodyBuilder();

        //    foreach (var soa in soas)
        //    {
        //        using (var tmpFile = PdfReport.CreateSoaTempPdf(soa))
        //        {
        //            byte[] bytes = File.ReadAllBytes(tmpFile.Path);
        //            string fileName = soa.Company.Name + "_" + soa.SoaDate.ToString("MMMM") + "_" + DateTime.Now.Ticks + ".pdf";
        //            bodyBuilder.Attachments.Add(fileName, bytes, ContentType.Parse("Application/pdf"));
        //        }
        //    }

        //    return bodyBuilder.Attachments;
        //}

        public static List<TempFile> CreateSoaFiles(List<Soa> soas)
        {
            if (soas == null || soas.Count == 0)
            {
                return null;
            }

            List<TempFile> files = new List<TempFile>();

            foreach (var soa in soas)
            {
                var tmpFile = PdfReport.CreateSoaTempPdf(soa);
                files.Add(tmpFile);
                
            }

            return files;
        }

        public static void SendEmail(MimeMessage email)
        {

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            using (var client = new SmtpClient())
            {
                try
                {

                    
                    client.Connect(config.AppSettings.Settings["Host"].Value, int.Parse(config.AppSettings.Settings["Port"].Value), SecureSocketOptions.StartTlsWhenAvailable);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(config.AppSettings.Settings["Email"].Value, config.AppSettings.Settings["Password"].Value);
                    client.Send(email);
                }
                catch
                {
                    throw new Exception("Failed to connect to email.");
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        public static void TestConnecton(string host, int port, string email, string password)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(host, port, SecureSocketOptions.StartTlsWhenAvailable);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(email, password);
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to connect : "+ e.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
