using SWNI.Common;
using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public interface ICashDonationService : IDisposable
    {
        CashDonation Insert(CashDonation CashDonation);
        bool Exists(CashDonation CashDonation);
        CashDonation FindById(int id);
        bool Update(CashDonation CashDonation);
        bool Delete(CashDonation CashDonation);
        int Count();
        IEnumerable<CashDonation> GetAll();
        IEnumerable<CashDonation> GetByUser(string userName);
        IEnumerable<CashDonation> GetByUser(string userName, int count, bool mostRecent = true);
        IEnumerable<CashDonation> GetByDate(DateTime date);
        IEnumerable<CashDonation> GetByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<CashDonation> GetByPartOfYear(int year, PartOfYear part, int partNumber);
        IEnumerable<CashDonation> GetByYearRange(int startYear, int endYear);
        decimal GetTotal();  
    }
}
