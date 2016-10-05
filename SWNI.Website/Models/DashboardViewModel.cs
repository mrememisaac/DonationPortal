using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWNI.Website.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            this.LastTenDonations = new HashSet<CashDonation>();
        }

        public string Name { get; set; }

        public IEnumerable<CashDonation> LastTenDonations { get; set; }       
    }
}