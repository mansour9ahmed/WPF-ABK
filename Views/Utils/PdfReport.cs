using GemBox.Spreadsheet;
using Microsoft.WindowsAPICodePack.Dialogs;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ViewModels;

namespace Utils
{
    public static class PdfReport
    {

        private static void CreateInvoice(Invoice invoice, string destFullPath)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            string tmpPath = Path.GetTempPath() + "bmInvoice.xlsx";

            if (!File.Exists(tmpPath))
            {
                MemoryStream ms = new MemoryStream();

                Assembly assembly = Assembly.GetExecutingAssembly();
                assembly.GetManifestResourceStream("Views.Resources.Invoice.xlsx").CopyTo(ms);
                Byte[] bArray = ms.ToArray();
                File.WriteAllBytes(tmpPath, bArray);
            }



            var workbook = ExcelFile.Load(tmpPath);
            var excelWorksheet = workbook.Worksheets[0];


            excelWorksheet.Cells[7, 1].Value = invoice.InvoiceNo;
            excelWorksheet.Cells[8, 1].Value = invoice.Date;
            excelWorksheet.Cells[10, 1].Value = invoice.Vessel.Name;
            excelWorksheet.Cells[11, 1].Value = invoice.Vessel.SimNo;
            excelWorksheet.Cells[12, 1].Value = invoice.Vessel.Email;

            excelWorksheet.Cells[16, 1].Value = invoice.BundleName;
            excelWorksheet.Cells[16, 3].Value = invoice.BundlePrice;

            excelWorksheet.Cells[18, 1].Value = invoice.OverMin;
            excelWorksheet.Cells[18, 2].Value = invoice.OverMinPrice;
            excelWorksheet.Cells[18, 3].Value = invoice.MinPrice;


            excelWorksheet.Cells[20, 1].Value = invoice.OverMb;
            excelWorksheet.Cells[20, 2].Value = invoice.OverMbPrice;
            excelWorksheet.Cells[20, 3].Value = invoice.MbPrice;

            excelWorksheet.Cells[22, 3].Value = invoice.IntegraPrice;
            excelWorksheet.Cells[24, 3].Value = invoice.Total;

            excelWorksheet.Cells[32, 1].Value = invoice.Bank.Name;
            excelWorksheet.Cells[33, 1].Value = invoice.Bank.Address;
            excelWorksheet.Cells[34, 1].Value = invoice.Bank.BeneficiaryName;
            excelWorksheet.Cells[35, 1].Value = invoice.Bank.UsdAccountNo;
            excelWorksheet.Cells[36, 1].Value = invoice.Bank.UsdIban;
            excelWorksheet.Cells[37, 1].Value = invoice.Bank.EuroAccountNo;
            excelWorksheet.Cells[38, 1].Value = invoice.Bank.EuroIban;
            excelWorksheet.Cells[39, 1].Value = invoice.Bank.Swift;


            workbook.Save(destFullPath);
        }

        public static void SaveInoiceAsPdf(Invoice invoice, string destPath)
        {
            string path = destPath + "/" + invoice.Vessel.Name + "_" + invoice.Date.ToString("MMMM")+ "_" + DateTime.UtcNow.Ticks+".pdf";

            CreateInvoice(invoice, path);
        }

        public static TempFile CreateInvoiceTempPdf(Invoice invoice)
        {
            string path = Path.GetTempPath()  + invoice.Vessel.Name + "_" + invoice.Date.ToString("MMMM")+"_" + DateTime.UtcNow.Ticks + ".pdf";
            var tmpFile = new TempFile(path);
             CreateInvoice(invoice, path);

            return tmpFile;

        }



        //  SOA

        private static void CreateSoa(Soa soa, string destFullPath)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            string tmpPath = Path.GetTempPath() + "bmSoa.xlsx";

            if (!File.Exists(tmpPath))
            {
                MemoryStream ms = new MemoryStream();

                Assembly assembly = Assembly.GetExecutingAssembly();
                assembly.GetManifestResourceStream("Views.Resources.SOA.xlsx").CopyTo(ms);
                Byte[] bArray = ms.ToArray();
                File.WriteAllBytes(tmpPath, bArray);
            }



            var workbook = ExcelFile.Load(tmpPath);
            var excelWorksheet = workbook.Worksheets[0];


            excelWorksheet.Cells[7, 1].Value = soa.SoaNo;
            excelWorksheet.Cells[8, 1].Value = soa.SoaDate;
            excelWorksheet.Cells[10, 1].Value = soa.Company.Name;


            int i = 0;

            if (soa.SoaElements != null && soa.SoaElements.Count > 0)
            {
                excelWorksheet.Cells[16, 0].Value = soa.SoaElements[0].Name;
                excelWorksheet.Cells[16, 3].Value = soa.SoaElements[0].Price;

                foreach (var tmp in soa.SoaElements.Skip(1))
                {
                    int row = 18 + i;

                    excelWorksheet.Cells.GetSubrangeAbsolute(row, 0, row + 1, 2).Merged = true;
                    excelWorksheet.Cells.GetSubrangeAbsolute(row, 3, row + 1, 3).Merged = true;

                    excelWorksheet.Cells[row, 0].Style = excelWorksheet.Cells[17, 0].Style;
                    excelWorksheet.Cells[row, 0].Value = tmp.Name;
                    excelWorksheet.Cells[row, 3].Style = excelWorksheet.Cells[17, 3].Style;
                    excelWorksheet.Cells[row, 3].Value = tmp.Price;
                    i += 2;
                }
            }


            excelWorksheet.Cells.GetSubrangeAbsolute(18 + i, 2, 19 + i, 2).Merged = true;
            excelWorksheet.Cells.GetSubrangeAbsolute(18 + i, 3, 19 + i, 3).Merged = true;

            excelWorksheet.Cells[18 + i, 2].Style.FillPattern = excelWorksheet.Cells[14, 0].Style.FillPattern;
            excelWorksheet.Cells[18 + i, 2].Style.Font = excelWorksheet.Cells[14, 0].Style.Font;
            excelWorksheet.Cells[18 + i, 2].Style.HorizontalAlignment = excelWorksheet.Cells[14, 0].Style.HorizontalAlignment;
            excelWorksheet.Cells[18 + i, 2].Style.VerticalAlignment = excelWorksheet.Cells[14, 0].Style.VerticalAlignment;
            excelWorksheet.Cells[18 + i, 2].Value = "TOTAL";
            excelWorksheet.Cells[18 + i, 3].Style.FillPattern = excelWorksheet.Cells[14, 0].Style.FillPattern;
            excelWorksheet.Cells[18 + i, 3].Style.Font = excelWorksheet.Cells[14, 0].Style.Font;
            excelWorksheet.Cells[18 + i, 3].Style.HorizontalAlignment = excelWorksheet.Cells[14, 0].Style.HorizontalAlignment;
            excelWorksheet.Cells[18 + i, 3].Style.VerticalAlignment = excelWorksheet.Cells[14, 0].Style.VerticalAlignment;
            excelWorksheet.Cells[18 + i, 3].Value = soa.Total;
            excelWorksheet.Cells[18 + i, 3].Style.NumberFormat = excelWorksheet.Cells[16,3].Style.NumberFormat;

            excelWorksheet.Cells[38, 1].Value = soa.Bank.Name;
            excelWorksheet.Cells[39, 1].Value = soa.Bank.Address;
            excelWorksheet.Cells[40, 1].Value = soa.Bank.BeneficiaryName;
            excelWorksheet.Cells[41, 1].Value = soa.Bank.UsdAccountNo;
            excelWorksheet.Cells[42, 1].Value = soa.Bank.UsdIban;
            excelWorksheet.Cells[43, 1].Value = soa.Bank.EuroAccountNo;
            excelWorksheet.Cells[44, 1].Value = soa.Bank.EuroIban;
            excelWorksheet.Cells[45, 1].Value = soa.Bank.Swift;


            workbook.Save(destFullPath);
        }

        public static void SaveSoaAsPdf(Soa soa, string destPath)
        {
            string path = destPath + "/SOA_" + soa.Company.Name + "_" + soa.SoaDate.ToString("MMMM") + "_" + DateTime.UtcNow.Ticks + ".pdf";

            CreateSoa(soa, path);
        }

        public static void SaveSoaAsExcel(Soa soa, string destPath)
        {
            string path = destPath + "/SOA_" + soa.Company.Name + "_" + soa.SoaDate.ToString("MMMM") + "_" + DateTime.UtcNow.Ticks + ".xlsx";

            CreateSoa(soa, path);
        }

        public static TempFile CreateSoaTempPdf(Soa soa)
        {
            string path = Path.GetTempPath() + "/" + soa.Company.Name + "_" + soa.SoaDate.ToString("MMMM") + "_" + DateTime.UtcNow.Ticks + ".pdf";
            var tmpFile = new TempFile(path);
            CreateSoa(soa, path);

            return tmpFile;

        }


        // TRANSACTIONS
        private static void CreateTransactionReport(TransactionAccount account, string destFullPath)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            string tmpPath = Path.GetTempPath() + "bmTa.xlsx";

            if (!File.Exists(tmpPath))
            {
                MemoryStream ms = new MemoryStream();

                Assembly assembly = Assembly.GetExecutingAssembly();
                assembly.GetManifestResourceStream("Views.Resources.Transaction.xlsx").CopyTo(ms);
                Byte[] bArray = ms.ToArray();
                File.WriteAllBytes(tmpPath, bArray);
            }



            var workbook = ExcelFile.Load(tmpPath);
            var excelWorksheet = workbook.Worksheets[0];


            excelWorksheet.Cells[8, 1].Value = DateTime.Now.ToString("dd-MMM-yyyy");
            excelWorksheet.Cells[10, 1].Value = account.Name;


            int i = 0;

            if (account.Transactions != null && account.Transactions.Count > 0)
            {
                account.Transactions = account.Transactions.OrderBy(t => t.IsInput).ToList();
                excelWorksheet.Cells[16, 0].Value = account.Transactions[0].Date.ToString("dd-MMM-yyyy");
                excelWorksheet.Cells[16, 2].Value = account.Transactions[0].IsInput? "Recieved":"Paid";
                excelWorksheet.Cells[16, 3].Value = account.Transactions[0].Amount;

                foreach (var tmp in account.Transactions.Skip(1))
                {
                    int row = 18 + i;

                    excelWorksheet.Cells.GetSubrangeAbsolute(row, 0, row + 1, 1).Merged = true;
                    excelWorksheet.Cells.GetSubrangeAbsolute(row, 2, row + 1, 2).Merged = true;
                    excelWorksheet.Cells.GetSubrangeAbsolute(row, 3, row + 1, 3).Merged = true;

                    excelWorksheet.Cells[row, 0].Style = excelWorksheet.Cells[17, 0].Style;
                    excelWorksheet.Cells[row, 0].Value = tmp.Date.ToString("dd-MMM-yyyy");
                    excelWorksheet.Cells[row, 2].Style = excelWorksheet.Cells[17, 2].Style;
                    excelWorksheet.Cells[row, 2].Value = tmp.IsInput?"Recieved":"Paid";
                    excelWorksheet.Cells[row, 3].Style = excelWorksheet.Cells[17, 3].Style;
                    excelWorksheet.Cells[row, 3].Value = tmp.Amount;
                    i += 2;
                }
            }


            excelWorksheet.Cells.GetSubrangeAbsolute(18 + i, 2, 19 + i, 2).Merged = true;
            excelWorksheet.Cells.GetSubrangeAbsolute(18 + i, 3, 19 + i, 3).Merged = true;

            excelWorksheet.Cells[18 + i, 2].Style.FillPattern = excelWorksheet.Cells[14, 0].Style.FillPattern;
            excelWorksheet.Cells[18 + i, 2].Style.Font = excelWorksheet.Cells[14, 0].Style.Font;
            excelWorksheet.Cells[18 + i, 2].Style.HorizontalAlignment = excelWorksheet.Cells[14, 0].Style.HorizontalAlignment;
            excelWorksheet.Cells[18 + i, 2].Style.VerticalAlignment = excelWorksheet.Cells[14, 0].Style.VerticalAlignment;
            excelWorksheet.Cells[18 + i, 2].Value = "TOTAL";
            excelWorksheet.Cells[18 + i, 3].Style.FillPattern = excelWorksheet.Cells[14, 0].Style.FillPattern;
            excelWorksheet.Cells[18 + i, 3].Style.Font = excelWorksheet.Cells[14, 0].Style.Font;
            excelWorksheet.Cells[18 + i, 3].Style.HorizontalAlignment = excelWorksheet.Cells[14, 0].Style.HorizontalAlignment;
            excelWorksheet.Cells[18 + i, 3].Style.VerticalAlignment = excelWorksheet.Cells[14, 0].Style.VerticalAlignment;
            excelWorksheet.Cells[18 + i, 3].Value = account.Total;
            excelWorksheet.Cells[18 + i, 3].Style.NumberFormat = excelWorksheet.Cells[16, 3].Style.NumberFormat;

            workbook.Save(destFullPath);
        }

        public static void SaveTransactionAccountAsPdf(TransactionAccount account, string destPath)
        {
            string path = destPath + "/Report_" + account.Name + "_" +  DateTime.UtcNow.Ticks + ".pdf";

            CreateTransactionReport(account, path);
        }


    }
}
