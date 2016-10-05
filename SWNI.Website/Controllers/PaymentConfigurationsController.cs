using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SWNI.Data;
using SWNI.Entities;
using SWNI.Services;

namespace SWNI.Website.Controllers
{
    [Authorize(Roles="Admin")]
    public class PaymentConfigurationsController : Controller
    {        
        private IPaymentConfigurationService configService;

        public PaymentConfigurationsController(IPaymentConfigurationService configService)
        {
            this.configService = configService;
        }

        // GET: PaymentConfigurations
        public async Task<ActionResult> Index()
        {
            var items = await Task.Run(() => configService.GetAll().ToList());
            //foreach (var paymentConfiguration in items)
            //{
            //    paymentConfiguration.Secret = StringEncrypterDecrypter.StringCipher.Decrypt(paymentConfiguration.Secret.Trim(), String.Format("{0}{1}{2}{3}{4}{5}{6}", User.Identity.Name, paymentConfiguration.DateCreated.Year, paymentConfiguration.DateCreated.Month, paymentConfiguration.DateCreated.Day, paymentConfiguration.DateCreated.Hour, paymentConfiguration.DateCreated.Minute, paymentConfiguration.DateCreated.Second, paymentConfiguration.DateCreated.Millisecond)).Substring(0,10);
            //}
            return View(items);
        }

        // GET: PaymentConfigurations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentConfiguration paymentConfiguration = await Task.Run(() => configService.Get(id.Value)); // db.PaymentConfigurations.FindAsync(id);
            if (paymentConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(paymentConfiguration);
        }

        // GET: PaymentConfigurations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentConfigurations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Secret")] PaymentConfiguration paymentConfiguration)
        {
            paymentConfiguration.DateCreated = DateTime.Now;
            paymentConfiguration.CreatedBy = User.Identity.Name;
            //paymentConfiguration.Secret = StringEncrypterDecrypter.StringCipher.Encrypt(paymentConfiguration.Secret, String.Format("{0}{1}{2}{3}{4}{5}{6}", User.Identity.Name, paymentConfiguration.DateCreated.Year, paymentConfiguration.DateCreated.Month, paymentConfiguration.DateCreated.Day, paymentConfiguration.DateCreated.Hour, paymentConfiguration.DateCreated.Minute, paymentConfiguration.DateCreated.Second, paymentConfiguration.DateCreated.Millisecond));
            if (ModelState.IsValid)
            {
                configService.Insert(paymentConfiguration);                
                return RedirectToAction("Index");
            }

            return View(paymentConfiguration);
        }

        // GET: PaymentConfigurations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentConfiguration paymentConfiguration = await Task.Run(() => configService.Get(id.Value)); 
            if (paymentConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(paymentConfiguration);
        }

        // POST: PaymentConfigurations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Secret")] PaymentConfiguration paymentConfiguration)
        {
            paymentConfiguration.UpdatedBy = User.Identity.Name;
            paymentConfiguration.DateUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                var original = configService.Get(paymentConfiguration.Id);
                if (original != null)
                {
                    original.Secret = paymentConfiguration.Secret; // StringEncrypterDecrypter.StringCipher.Encrypt(paymentConfiguration.Secret, String.Format("{0}{1}{2}{3}{4}{5}{6}", User.Identity.Name, paymentConfiguration.DateCreated.Year, paymentConfiguration.DateCreated.Month, paymentConfiguration.DateCreated.Day, paymentConfiguration.DateCreated.Hour, paymentConfiguration.DateCreated.Minute, paymentConfiguration.DateCreated.Second, paymentConfiguration.DateCreated.Millisecond));
                }
                configService.Update(paymentConfiguration);
                return RedirectToAction("Index");
            }
            return View(paymentConfiguration);
        }

        // GET: PaymentConfigurations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentConfiguration paymentConfiguration = await Task.Run(() => configService.Get(id.Value)); 
            if (paymentConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(paymentConfiguration);
        }

        // POST: PaymentConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PaymentConfiguration paymentConfiguration = await Task.Run(() => configService.Get(id));
            await Task.Run(() => configService.Delete(paymentConfiguration));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                configService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
