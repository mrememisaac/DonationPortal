using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using PaymentGateway.PayStack;
using SWNI.Services;
using SWNI.Entities;

namespace SWNI.Website.Controllers
{
    /// <summary>
    /// 1. Create a top up
    /// 2. View my top ups
    /// </summary>
    [Authorize]
    public class DonateController : Controller
    {
        private readonly ICashDonationService topUpService;
        private readonly IDonationUnitsService unitsService;
        private readonly ICashDonationAttemptService cashDonationAttemptService;
        private readonly IPayStackServices paystackService;
        private readonly IPaymentConfigurationService payStackConfiguration;
        private ApplicationUserManager _userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topUpService"></param>
        /// <param name="subscriberService"></param>
        /// <param name="unitsService"></param>
        /// <param name="cashDonationAttemptService"></param>
        /// <param name="paystackService"></param>
        /// <param name="paystackConfig"></param>
        public DonateController(ICashDonationService topUpService, IDonationUnitsService unitsService, ICashDonationAttemptService cashDonationAttemptService, IPayStackServices paystackService, IPaymentConfigurationService paystackConfig, ApplicationUserManager userManager)
        {
            this.topUpService = topUpService;
            this.cashDonationAttemptService = cashDonationAttemptService;
            this.unitsService = unitsService;
            this.paystackService = paystackService;
            this.payStackConfiguration = paystackConfig;
            this._userManager = userManager;
        }

        /// <summary>
        /// Lists the users top ups
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //1. Get my subscriber id           
            if (User.Identity.IsAuthenticated)
            {
                return View(topUpService.GetByUser(User.Identity.Name));
            }
            ViewBag.Message = "You do not have a subscription";
            return View("AccessDenied");
        }

        [HttpGet]
        public ActionResult Donate()
        {
            return View(unitsService.Get());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Donate(decimal Units)
        {
            if (unitsService.Get(Units) != null)
            {
                //get the subscriber
                var account = await _userManager.FindByNameAsync(User.Identity.Name);

                if (account != null)
                {
                    //log the transaction
                    CashDonationAttempt attempt = new CashDonationAttempt()
                    {
                        Email = account.Email,
                        PhoneNumber = account.PhoneNumber,
                        Amount = Units,
                        CreatedBy = User.Identity.Name,
                        DateCreated = DateTime.Now,
                        Reference = Guid.NewGuid().ToString()
                    };

                    Session.Add("trxref", attempt.Reference);

                    //Save the transaction
                    attempt = cashDonationAttemptService.Insert(attempt);

                    //create transaction                    
                    var transaction = new TransactionInitialize()
                    {
                        Amount = Units,
                        Email = account.Email,
                        Reference = attempt.Reference,
                        CallBackUrl = String.Format("{0}/{1}/{2}", Request.Url.GetLeftPart(UriPartial.Authority), "Donate", "Verify")
                    };

                    //decrypt secret
                    var paymentConfiguration = payStackConfiguration.GetDefault();
                    var secret = paymentConfiguration.Secret;//StringEncrypterDecrypter.StringCipher.Decrypt(paymentConfiguration.Secret, String.Format("{0}{1}{2}{3}{4}{5}{6}", User.Identity.Name, paymentConfiguration.DateCreated.Year, paymentConfiguration.DateCreated.Month, paymentConfiguration.DateCreated.Day, paymentConfiguration.DateCreated.Hour, paymentConfiguration.DateCreated.Minute, paymentConfiguration.DateCreated.Second, paymentConfiguration.DateCreated.Millisecond));
                   
                    //request charge authorization                    
                    var canCharge = await paystackService.Initialize(secret, transaction);
                    if (canCharge == null)
                    {
                        return RedirectToAction("Donate");
                    }
                    if (canCharge.Successful)
                    {
                        //redirect the user to the charge page
                        return Redirect(canCharge.Data.AuthorizationUrl);
                    }
                }
                else
                {
                    ViewBag.Message = "Only Registered users are allowed to top up";
                    return View("AccessDenied");
                }
            }
            return View();
        }

        public async Task<ActionResult> Verify(string trxref)
        {
            if (string.IsNullOrEmpty(trxref) == true)
            {
                if (Session["trxref"] != null)
                {
                    trxref = Session["trxref"].ToString();
                }
            }
            //verify that we have the reference
            var attempt = cashDonationAttemptService.GetByReference(trxref);

            var user = _userManager.FindByNameAsync(User.Identity.Name);
            if (attempt != null)
            {
                //oh great! We got the money, ok now lets confirm
                var verifyResponse = await paystackService.VerifyCharge(payStackConfiguration.GetDefault().Secret, attempt.Reference);

                if (verifyResponse.Successful)
                {
                    //excellent, confirmed so 

                    //1. Update the attempt
                    attempt.IsSuccessful = true;
                    attempt.CardType = verifyResponse.Data.Authorization.CardType;
                    attempt.AuthorizationCode = verifyResponse.Data.Authorization.AuthorizationCode;
                    attempt.Bank = verifyResponse.Data.Authorization.Bank;
                    attempt.Last4Digits = verifyResponse.Data.Authorization.Last4Digits;
                    attempt.Amount = verifyResponse.Data.Amount / 100; //api returns amount in kobo, we divide by 100 to get the naira equivalent
                    attempt.DateUpdated = DateTime.Now;
                    attempt.UpdatedBy = User.Identity.Name;

                    //save changes
                    cashDonationAttemptService.Update(attempt);

                    //2. Credit the subscriber                                
                    CashDonation topUp = new CashDonation { Amount = attempt.Amount, CreatedBy = attempt.CreatedBy, Date = DateTime.Now, DateCreated = DateTime.Now, CashDonationAttemptId = attempt.Id };
                    topUpService.Insert(topUp);

                    return View("Status", true);//RedirectToAction("Index", "Dashboard");
                }
            }

            return View("Status", false);
        }
    }
}