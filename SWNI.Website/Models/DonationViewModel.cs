using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Website.Models
{
    public class DonationViewModel
    {
        public DonationViewModel()
        {
            this.QuarterlyDonation = new HashSet<QuarterlyDonation>();
            this.AnnualDonation = new HashSet<AnnualDonation>();
            this.DonationByAgency = new HashSet<DonationByAgency>();
            this.MonthlyDonation = new HashSet<MonthlyDonation>();
        }

        public decimal TotalDonation { get; set; }        
        public decimal Receipts { get; set; }
        public decimal ThisMonthsDonation { get; set; }
        public decimal ThisQuartersDonation { get; set; }
        public decimal ThisYearsDonation { get; set; }

        public ICollection<DonationByAgency> DonationByAgency { get; set; }

        public ICollection<AnnualDonation> AnnualDonation { get; set; }

        public ICollection<QuarterlyDonation> QuarterlyDonation { get; set; }

        public ICollection<MonthlyDonation> MonthlyDonation { get; set; }
    }

    public class DonationByAgency
    {
        public string Agency { get; set; }
        public decimal Amount { get; set; }
    }

    public class AnnualDonation
    {
        public int Year { get; set; }
        public decimal Donation { get; set; }
    }

    public class QuarterlyDonation : AnnualDonation
    {   
        public int Quarter { get; set; }    
    }

    public class MonthlyDonation : AnnualDonation
    {
        public string Month { get; set; }
    }
}
