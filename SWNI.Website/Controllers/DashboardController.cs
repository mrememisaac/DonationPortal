using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWNI.Entities;
using SWNI.Services;
using System.Threading.Tasks;
using SWNI.Website.Models;

namespace SWNI.Website.Controllers
{
    [Authorize]
    public class DashboardController : Controller, IDisposable
    {
        private ApplicationUserManager userManager;
        private readonly ICashDonationService topupService;


        public DashboardController(ICashDonationService topupService, ApplicationUserManager userManager)
        {
            this.topupService = topupService;
            this.userManager = userManager;

        }

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var subscriber = await Task.Run(() => userManager.FindByNameAsync(User.Identity.Name));
                if (subscriber == null)
                {
                    //get him to update his details
                    //return RedirectToAction("Subscribe", "Subscribers");
                }

                DashboardViewModel dashView = new DashboardViewModel()
                {
                    Name = subscriber.Name,
                    LastTenDonations = topupService.GetByUser(User.Identity.Name, 20)
                };

                return View(dashView);

            }
            return View("AccessDenied");
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public new virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.topupService.Dispose();
                base.Dispose();
            }
        }
    }
}