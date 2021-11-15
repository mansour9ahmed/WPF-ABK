using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IInvoiceService
    {
        void CreateInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
        bool DeleteInvoice(int id);
        List<Invoice> SearchForInvoices(DateTime FromDate, DateTime ToDate,bool? isPaid,int? VesselId,int? CompanyId);
        bool SetPaid(int invoiceId);
        List<BankAccount> GetAllBankAccounts();
    }
}
