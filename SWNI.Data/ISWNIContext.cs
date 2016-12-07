using SWNI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Data
{
    public interface ISWNIContext : IDisposable
    {
        /// <summary>
        /// List of Cash Donations
        /// </summary>
        DbSet<CashDonation> CashDonations { get; set; }

        /// <summary>
        /// List of cash donation attempts
        /// </summary>
        DbSet<CashDonationAttempt> CashDonationAttempts { get; set; }

        /// <summary>
        /// List of items the cause wants to use items on
        /// </summary>
        DbSet<ItemCost> ItemCosts { get; set; }

        /// <summary>
        /// List of Payment Processor Configurations
        /// </summary>
        DbSet<PaymentConfiguration> PaymentConfigurations { get; set; }

        /// <summary>
        /// List of employees
        /// </summary>
        DbSet<Employee> Employees { get; set; }
    }
}
