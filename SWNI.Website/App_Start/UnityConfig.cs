using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using SWNI.Website.Controllers;
using Microsoft.Owin.Security;
using System.Web;
using SWNI.Website.Models;
using SWNI.Data;

namespace SWNI.Website
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<Services.IDonationsService, Services.DonationsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<PaymentGateway.PayStack.IPayStackServices, PaymentGateway.PayStack.PayStackService>(new ContainerControlledLifetimeManager());
            container.RegisterType<Services.IEmployeeService, Services.EmployeeService>(new ContainerControlledLifetimeManager());
            container.RegisterType<Services.IDonationUnitsService, Services.DonationUnitsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<Services.ICashDonationService, Services.CashDonationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<Services.ICashDonationAttemptService, Services.CashDonationAttemptService>(new ContainerControlledLifetimeManager());
            container.RegisterType<Services.IItemCostService, Services.ItemCostService>(new ContainerControlledLifetimeManager());
            container.RegisterType<Services.IPaymentConfigurationService, Services.PaymentConfigurationService>(new ContainerControlledLifetimeManager());

            /*Persistence*/
            container.RegisterType(typeof(Repository<>), new InjectionConstructor(typeof(IUnitOfWork)));
            container.RegisterType(typeof(IUnitOfWork), typeof(SWNIContext), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>), new ContainerControlledLifetimeManager());
            container.RegisterType<SWNIContext>(new InjectionConstructor("SWNIContext"));

            /*asp.net Identity*/
            container.RegisterType(typeof(UserManager<>), new InjectionConstructor(typeof(IUserStore<>)));
            container.RegisterType(typeof(IUserStore<>), typeof(UserStore<>));
            container.RegisterType<Microsoft.AspNet.Identity.IUser>(new InjectionFactory(c => c.Resolve<Microsoft.AspNet.Identity.IUser>()));
            container.RegisterType<IdentityUser, ApplicationUser>(new ContainerControlledLifetimeManager());
            container.RegisterType<DbContext, ApplicationDbContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<ManageController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}