using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SWNI.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        public ActionResult Purchase()
        {
            return View(unitsService.Get());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Purchase(decimal Units)
        {
            if (unitsService.Get(Units) != null)
            {
                //get the subscriber
                var account = subscriberService.FindByUserName(User.Identity.Name);


                if (account != null)
                {
                    //log the transaction
                    TopUpAttempt attempt = new TopUpAttempt()
                    {
                        Email = emailAddressService.List(account.Id).FirstOrDefault(x => x.Ok).Text,
                        PhoneNumber = phoneNumberService.List(account.Id).FirstOrDefault(x => x.Ok).Digits,
                        Amount = Units,
                        CreatedBy = User.Identity.Name,
                        DateCreated = DateTime.Now,
                        Reference = Guid.NewGuid().ToString(),
                        SubscriberId = account.Id,
                    };

                    Session.Add("trxref", attempt.Reference);

                    //Save the transaction
                    attempt = topUpAttemptService.Insert(attempt);

                    

                    //create transaction                    
                    var transaction = new TransactionInitialize()
                    {
                        Amount = Units,
                        Email = emailAddressService.List(account.Id).FirstOrDefault(x => x.Ok).Text,
                        Reference = attempt.Reference,
                        CallBackUrl = String.Format("{0}/{1}/{2}", Request.Url.GetLeftPart(UriPartial.Authority), "TopUp", "Verify")
                    };

                    //request charge authorization                    
                    var canCharge = await paystackService.Initialize(payStackConfiguration.GetDefault().Secret, transaction);
                    if (canCharge == null)
                    {
                        return RedirectToAction("Purchase");
                    }
                    if (canCharge.Successful)
                    {
                        //redirect the user to the charge page
                        return Redirect(canCharge.Data.AuthorizationUrl);
                    }
                    //var page = paystfackService.GetPaymentPages().FirstOrDefault(x => x.Amount == Units);
                    //if (page != null)
                    //{
                    //    return Redirect(page.Url);
                    //}
                }
                else
                {
                    ViewBag.Message = "Only subscribers are allowed to top up";
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
            var attempt = topUpAttemptService.GetByReference(trxref);

            var user = subscriberService.FindByUserName(User.Identity.Name);
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
                    topUpAttemptService.Update(attempt);

                    //2. Credit the subscriber                                
                    TopUp topUp = new TopUp { Amount = attempt.Amount, CreatedBy = attempt.CreatedBy, Date = DateTime.Now, DateCreated = DateTime.Now, SubscriberId = attempt.SubscriberId, TopUpAttemptId = attempt.Id };
                    topUpService.Insert(topUp);

                    return View("Status", true);//RedirectToAction("Index", "Dashboard");
                }
            }

            return View("Status", false);
        }
    }
}
