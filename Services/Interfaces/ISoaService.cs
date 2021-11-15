using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ISoaService
    {
        public void CreateSoa(Soa soa);
        public SoaElement CreateSoaElement(SoaElement element);
        public void UpdateSoa(Soa soa);
        public void UpdateSoaElement(SoaElement element);
        public void DeleteSoa(int id);
        public void DeleteSoaElement(int id);

        public void SetPaid(int id);
        public List<Soa> SearchForSoa(int? companyId, DateTime date, bool? isPiad);
        public List<SoaElement> GetElementsByCompany(int companyId);
        public List<SoaElement> GetPurchasesByCompany(int companyId, bool? isPaid);

    }
}
