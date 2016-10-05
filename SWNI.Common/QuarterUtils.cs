using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Common
{
    /// <summary>
    /// A utility class for functions relevant to quarters of a year
    /// </summary>
    public static class QuarterUtils
    {
        /// <summary>
        /// Returns the previous quarter given the current quarter (of a year)
        /// </summary>
        /// <param name="thisQuarter"></param>
        /// <returns></returns>
        public static int GetPreviousQuarter(int thisQuarter)
        {
            switch (thisQuarter)
            {
                case 1:
                    return 4;                    
                case 2:
                    return 1;                    
                case 3:
                    return 2;
                case 4:
                    return 3;
                default:
                    throw new Exception("Invalid quarter");
            }
        }

        /// <summary>
        /// Gets the current quarter of a year using the supplied date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetQuarter(DateTime date)
        {
            if (date.Month > 0 && date.Month < 4)
            {
                return 1;
            }
            else if (date.Month > 3 && date.Month < 7)
            {
                return 2;
            }
            else if (date.Month > 6 && date.Month < 10)
            {
                return 3;
            }
            else if (date.Month > 9 && date.Month < 13)
            {
                return 4;
            }
            else
            {
                throw new Exception("Unknown month");
            }
        }

        /// <summary>
        /// Returns a list of integers representing the months in that quarter
        /// </summary>
        /// <param name="quarter"></param>
        /// <returns>E.g given an input of 1, it returns a list containing 1, 2, 3 as the months in the first quarter of the year</returns>
        public static List<int> GetMonthsInQuarter(int quarter)
        {
            if (quarter == 1)
            {
                return new List<int>() { 1, 2, 3 };
            }
            if (quarter == 2)
            {
                return new List<int>() { 4, 5, 6 };
            }
            if (quarter == 3)
            {
                return new List<int>() { 7, 8, 9 };
            }
            if (quarter == 4)
            {
                return new List<int>() { 10, 11, 12 };
            }
            else
            {
                throw new Exception("Unknown quarter");
            }
        }
    }
}
