using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Common
{
    /// <summary>
    /// A utility class for month related data and calculations
    /// </summary>
    public static class MonthUtils
    {
        public class YearAndMonth
        {
            public int Year { get; set; }
            public int Month { get; set; }
        }

        public static YearAndMonth GetLastMonth(DateTime today)
        {
            int year, month = 0;
            if (today.Month == 1)
            {
                year = today.Year - 1;
                month = 12;
            }
            else
            {
                year = today.Year;
                month = today.Month - 1;
            }
            return new YearAndMonth() { Year = year, Month = month };
        }
    }
}
