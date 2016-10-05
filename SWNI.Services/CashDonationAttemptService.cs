using SWNI.Data;
using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    /// <summary>
    /// Manages CashDonationAttempt related activities
    /// </summary>
    public class CashDonationAttemptService : ICashDonationAttemptService
    {
        public void Dispose()
        {
            this.Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.repository.Dispose();
            }
        }
        private readonly IRepository<CashDonationAttempt> repository;
        //private readonly ITopUpService topUpService;

        public CashDonationAttemptService(IRepository<CashDonationAttempt> repository) //ITopUpService topUpService)
        {
            //this.topUpService = topUpService;
            this.repository = repository;
        }

        public CashDonationAttempt Insert(CashDonationAttempt CashDonationAttempt)
        {
            if (CashDonationAttempt.Amount < 200)
            {
                throw new Exception("Transaction amount must not be less than 200");
            }
            return repository.Add(CashDonationAttempt);
        }

        public IEnumerable<CashDonationAttempt> GetByUser(string userName)
        {
            return repository.GetAll(x => x.CreatedBy.Trim().Equals(userName.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<CashDonationAttempt> GetAll()
        {
            return repository.GetAll();
        }

        public bool Update(CashDonationAttempt CashDonationAttempt)
        {
            repository.Update(CashDonationAttempt);
            return true;
        }

        public void Delete(CashDonationAttempt CashDonationAttempt)
        {
            repository.Delete(CashDonationAttempt);
        }


        public CashDonationAttempt Get(int id)
        {
            return repository.Get(id);
        }


        public CashDonationAttempt GetByReference(string reference)
        {
            return repository.GetAll(x => x.Reference.Trim().Equals(reference.Trim(), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        public CashDonationAttempt GetByUser(string userName, string reference)
        {
            return repository.GetAll(x => x.CreatedBy.Trim().Equals(userName.Trim(), StringComparison.InvariantCultureIgnoreCase) && x.Reference.Trim().Equals(reference.Trim(), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }
    }
}


