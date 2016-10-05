using DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Entities
{
    public class CashDonation : AuditedEntityBase
    {
        /// <summary>
        /// The amount paid
        /// </summary>
        [Precision(10, 2)]
        public decimal Amount { get; set; }

        /// <summary>
        /// The date and time of the top up
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The attempt that was successful and that gave birth to this topup 
        /// </summary>
        public int CashDonationAttemptId { get; set; }
    }
}
