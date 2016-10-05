using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWNI.Entities
{
    /*
     * Should users buy units or should they buy quantities of specific Services, i designed the system against units so i will go with that May 6, 2016
     */

    /// <summary>
    /// Blocks in which units can be purchased, block name must match unit name
    /// </summary>
    public class DonationUnit
    {
        public DonationUnit()
        {
            this.Description = new List<String>();
        }
        /// <summary>
        /// The block name eg 100 Units, 200 Units, 500, 1000, 5000, 10000, 50000
        /// </summary>
        public string Name { get; set; }

        /// The block name eg 100, 200, 500, 1000, 5000, 10000, 50000
        public decimal Price { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        public List<string> Description { get; set; }
    }
}