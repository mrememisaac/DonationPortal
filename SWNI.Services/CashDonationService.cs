using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWNI.Entities;
using SWNI.Data;

namespace SWNI.Services
{
    public class CashDonationService : ICashDonationService
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

        //private readonly IUnitOfWork context;
        private IRepository<CashDonation> repository;

        public CashDonationService(IRepository<CashDonation> repository)
        {
            //this.context = context;
            this.repository = repository;
        }

        public CashDonation Insert(CashDonation CashDonation)
        {
            if (CashDonation.Amount < 200)
            {
                throw new Exception("Transaction amount must not be less than 200");
            }
            return repository.Add(CashDonation);
        }

        public CashDonation FindById(int id)
        {
            return repository.Get(id);
        }

        public bool Exists(CashDonation CashDonation)
        {
            return repository.GetAll(x => x.Amount.Equals(CashDonation.Amount)
                && x.Date.Equals(CashDonation.Date)
                && x.CreatedBy.Trim().Equals(CashDonation.CreatedBy.Trim(), StringComparison.InvariantCultureIgnoreCase)).Any();
        }

        public bool Update(CashDonation CashDonation)
        {
            var original = this.FindById(CashDonation.Id);
            if (original != null)
            {
                //AutoMapper.Mapper.Map<CashDonation, CashDonation>(CashDonation, original);
            }
            repository.Update(original);
            return true;
        }

        public bool Delete(CashDonation CashDonation)
        {
            repository.Delete(CashDonation);
            return true;
        }

        public IEnumerable<CashDonation> GetAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<CashDonation> GetLast(int count)
        {
            var total = Count();
            return repository.GetAll().Skip(total - count);
        }

        public IEnumerable<CashDonation> GetAll(System.Linq.Expressions.Expression<Func<CashDonation, bool>> filter)
        {
            return repository.GetAll(filter);
        }

        public int Count()
        {
            return repository.Count();
        }


        public IEnumerable<CashDonation> GetByUser(string userName)
        {
            return repository.GetAll(x => x.CreatedBy.Trim().Equals(userName.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<CashDonation> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CashDonation> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CashDonation> GetByPartOfYear(int year, Common.PartOfYear part, int partNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CashDonation> GetByYearRange(int startYear, int endYear)
        {
            return repository.GetAll(x => x.Date.Year >= startYear && x.Date.Year <= endYear);
        }


        public IEnumerable<CashDonation> GetByUser(string userName, int count, bool mostRecent = true)
        {
            if (mostRecent)
            {
                return repository.GetAll(x => x.CreatedBy.Trim().Equals(userName.Trim(), StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(x => x.DateCreated).Take(count);
            }
            return repository.GetAll(x => x.CreatedBy.Trim().Equals(userName.Trim(), StringComparison.InvariantCultureIgnoreCase)).Take(count);
        }


        public decimal GetTotal()
        {
            return repository.GetAll().Sum(x => x.Amount);
        }
    }
}
