using SWNI.Common;
using SWNI.Data;
using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public class DonationsService : IDonationsService
    {
        public void Dispose()
        {
            this.Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.donations.Dispose();               
            }
        }

        private readonly IRepository<CashDonation> donations;        

        public DonationsService(IRepository<CashDonation> donations)
        {
            this.donations = donations;
        }

        public decimal TotalDonation()
        {
            decimal total = 0;
            if (donations.GetAll().Count() > 0)
            {
                total += donations.GetAll().Sum(x => x.Amount);
            }            
            return total;
        }

        public decimal TotalDonationThisMonth()
        {
            return TotalDonationByMonth(DateTime.Now.Year, DateTime.Now.Month);
        }

        public decimal TotalDonationThisQuarter()
        {
            return TotalDonationByQuarter(DateTime.Now.Year, QuarterUtils.GetQuarter(DateTime.Now));
        }

        public decimal TotalDonationThisWeek()
        {
            throw new NotImplementedException();
        }

        public decimal TotalDonationThisYear()
        {
            //DateTime today = DateTime.Now;
            //return repository.GetAll(x => x.DateCreated.Year == today.Year).Sum(x => x.Charge);
            return TotalDonationByYear(DateTime.Now.Year);
        }

        public decimal TotalDonationLastMonth()
        {
            var yearAndMonth = Common.MonthUtils.GetLastMonth(DateTime.Now);
            return TotalDonationByMonth(yearAndMonth.Year, yearAndMonth.Month);
        }

        public decimal TotalDonationLastWeek()
        {
            throw new NotImplementedException();
        }

        public decimal TotalDonationLastQuarter()
        {
            return TotalDonationByLastQuarter(DateTime.Now.Year, Common.QuarterUtils.GetQuarter(DateTime.Now));
        }

        public decimal TotalDonationLastYear()
        {
            return TotalDonationByYear(DateTime.Now.Year - 1);
        }

        public decimal TotalDonationLastSixMonths()
        {
            int countDown = 6;
            decimal revenue = 0;
            DateTime today = DateTime.Now;
            while (countDown > 0)
            {
                int year = today.Year, month = today.Month;
                revenue += donations.GetAll(x => x.DateCreated.Year == year && x.DateCreated.Month == month).Sum(x => x.Amount);                
                if (month == 1)
                {
                    year = today.Year - 1;
                    month = 12;
                }
                countDown--;
            }
            return revenue;
        }

        public decimal TotalDonationByWeek(int year, int week)
        {
            throw new NotImplementedException();
        }

        public decimal TotalDonationByMonth(int year, int month)
        {
            decimal amt = 0;
            if (donations.GetAll(x => x.DateCreated.Year == year && x.DateCreated.Month == month).Count() > 0)
            {
                amt += donations.GetAll(x => x.DateCreated.Year == year && x.DateCreated.Month == month).Sum(x => x.Amount);
            }            
            return amt;
        }

        public decimal TotalDonationByQuarter(int year, int quarter)
        {
            decimal revenue = 0;
            var months = Common.QuarterUtils.GetMonthsInQuarter(quarter);
            foreach (var month in months)
            {
                if (donations.GetAll(x => x.DateCreated.Year == year && x.DateCreated.Month == month).Count() > 0)
                    revenue += donations.GetAll(x => x.DateCreated.Year == year && x.DateCreated.Month == month).Sum(x => x.Amount);
            }
            return revenue;
        }

        public decimal TotalDonationByLastQuarter(int thisYear, int thisQuarter)
        {
            decimal revenue = 0;
            if (DateTime.Now.Month == 1)
            {
                thisYear = DateTime.Now.Year - 1;
            }
            var lastQuarter = Common.QuarterUtils.GetPreviousQuarter(thisQuarter);
            var months = Common.QuarterUtils.GetMonthsInQuarter(lastQuarter);
            foreach (var month in months)
            {
                if (donations.GetAll(x => x.DateCreated.Year == thisYear && x.DateCreated.Month == month).Count() > 0)
                    revenue += donations.GetAll(x => x.DateCreated.Year == thisYear && x.DateCreated.Month == month).Sum(x => x.Amount);                
            }
            return revenue;
        }

        public decimal TotalDonationByYear(int year)
        {
            decimal amt = 0;
            if (donations.GetAll(x => x.DateCreated.Year == year).Count() > 0)
                amt += donations.GetAll(x => x.DateCreated.Year == year).Sum(x => x.Amount);            
            return amt;
        }

        public decimal TotalDonationByDate(DateTime date)
        {
            decimal amt = 0;            
            if (donations.GetAll(x => x.DateCreated.Year == date.Year && x.DateCreated.Month == date.Month && x.DateCreated.Day == date.Day).Count() > 0)
                amt += donations.GetAll(x => x.DateCreated.Year == date.Year && x.DateCreated.Month == date.Month && x.DateCreated.Day == date.Day).Sum(x => x.Amount);
            return amt;
        }

        public decimal TotalDonationByDateRange(DateTime startDate, DateTime endDate)
        {
            decimal amt = 0;
            int startYear = startDate.Year, startMonth = startDate.Month, endYear = endDate.Year, endMonth = endDate.Month;            
            if (donations.GetAll(x => x.DateCreated.Year >= startYear && x.DateCreated.Month >= startMonth && x.DateCreated.Year <= endDate.Year && x.DateCreated.Month <= endDate.Month).Count() > 0)
                amt += donations.GetAll(x => x.DateCreated.Year >= startYear && x.DateCreated.Month >= startMonth && x.DateCreated.Year <= endDate.Year && x.DateCreated.Month <= endDate.Month).Sum(x => x.Amount);
            return amt;
        }
    }
}
