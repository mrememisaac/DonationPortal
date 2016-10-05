using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Services
{
    public interface IDonationsService : IDisposable
    {
        decimal TotalDonation();
        decimal TotalDonationThisMonth();
        decimal TotalDonationThisQuarter();
        decimal TotalDonationThisWeek();
        decimal TotalDonationThisYear();

        decimal TotalDonationLastMonth();
        decimal TotalDonationLastWeek();
        decimal TotalDonationLastQuarter();
        decimal TotalDonationLastYear();
        decimal TotalDonationLastSixMonths();

        decimal TotalDonationByWeek(int year, int week);
        decimal TotalDonationByMonth(int year, int month);
        decimal TotalDonationByQuarter(int year, int quarter);
        decimal TotalDonationByLastQuarter(int year, int quarter);
        decimal TotalDonationByYear(int year);
        decimal TotalDonationByDate(DateTime date);
        decimal TotalDonationByDateRange(DateTime startDate, DateTime endDate);
    }
}
