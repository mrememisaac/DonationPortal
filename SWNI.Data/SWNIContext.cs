using DataAnnotations;
using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Data
{
    public class SWNIContext : SWNIContextBase, ISWNIContext
    {
        public SWNIContext()
            : base("SWNIContext")
        {

        }

        public SWNIContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<CashDonation> CashDonations { get; set; }
        public DbSet<CashDonationAttempt> CashDonationAttempts { get; set; }
        public DbSet<ItemCost> ItemCosts { get; set; }
        public DbSet<PaymentConfiguration> PaymentConfigurations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties().Where(x => x.GetCustomAttributes(false).OfType<Precision>().Any())
                .Configure(c => c.HasPrecision(c.ClrPropertyInfo.GetCustomAttributes(false).OfType<Precision>().First()
                    .precision, c.ClrPropertyInfo.GetCustomAttributes(false).OfType<Precision>().First().scale));
            base.OnModelCreating(modelBuilder);
        }

    }
}