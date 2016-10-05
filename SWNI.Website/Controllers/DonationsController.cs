using SWNI.Services;
using SWNI.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWNI.Website.Controllers
{
    [Authorize]
    public class DonationsController : Controller, IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                donationService.Dispose();                
            }
        }
        private readonly IDonationsService donationService;        

        public DonationsController(IDonationsService donationService)
        {
            this.donationService = donationService;            
        }

        // GET: Donation
        public ActionResult Index()
        {
            DonationViewModel model = new DonationViewModel();

            model.Receipts = donationService.TotalDonation();

            model.TotalDonation = donationService.TotalDonation();

            model.ThisMonthsDonation = donationService.TotalDonationThisMonth();

            model.ThisQuartersDonation = donationService.TotalDonationThisQuarter();

            model.ThisYearsDonation = donationService.TotalDonationThisYear();

            int count = 1;

            //donation by month for 2016
            string[] months = { "Fake", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            while (count < 13)
            {                
                model.MonthlyDonation.Add(new MonthlyDonation { Year = DateTime.Now.Year, Month = months[count], Donation = donationService.TotalDonationByMonth(DateTime.Now.Year, count) });
                count++;
            }


            //donation by quarter for 2016
            count = 1;
            while (count < 5)
            {
                model.QuarterlyDonation.Add(new QuarterlyDonation { Year = DateTime.Now.Year, Quarter = count, Donation = donationService.TotalDonationByQuarter(2016, count) });
                count++;
            }

            //donation per annum for 2012 to 2016
            int year = DateTime.Now.Year - 4;
            int endYear = DateTime.Now.Year;
            while (year <= endYear)
            {
                model.AnnualDonation.Add(new AnnualDonation { Year = year, Donation = donationService.TotalDonationByYear(year) });
                year++;
            }


            return View(model);
        }
    }
}