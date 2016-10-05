using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public interface ICashDonationAttemptService : IDisposable
    {
        CashDonationAttempt Insert(CashDonationAttempt CashDonationAttempt);
        CashDonationAttempt GetByReference(string reference);
        CashDonationAttempt GetByUser(string userName, string reference);
        IEnumerable<CashDonationAttempt> GetByUser(string userName);
        IEnumerable<CashDonationAttempt> GetAll();
        bool Update(CashDonationAttempt CashDonationAttempt);
        void Delete(CashDonationAttempt CashDonationAttempt);
        CashDonationAttempt Get(int id);
    }
}
