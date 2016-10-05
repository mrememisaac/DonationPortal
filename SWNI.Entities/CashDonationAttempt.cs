using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Entities
{
    public class CashDonationAttempt : AuditedEntityBase
    {
        /// <summary>
        /// Payment attempt reference
        /// </summary>
        [StringLength(100)]
        public string Reference { get; set; }

        /// <summary>
        /// The attempted purchase amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The status of an attempt if it succeeded or not
        /// </summary>
        public bool IsSuccessful { get; set; }

        [Phone]
        [StringLength(15)]
        [Required]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Reference provided by the payment gateway
        /// </summary>
        [StringLength(100)]
        public string PaymentGatewayReference { get; set; }

        /// <summary>
        /// Payment authorization code
        /// </summary>
        [StringLength(50)]
        public string AuthorizationCode { get; set; }

        /// <summary>
        /// Visa, Mastercard, Verve ...
        /// </summary>
        [StringLength(15)]
        public string CardType { get; set; }

        /// <summary>
        /// Last 4 card digits
        /// </summary>
        public int Last4Digits { get; set; }

        /// <summary>
        /// Bank
        /// </summary>
        [StringLength(50)]
        public string Bank { get; set; }
    }
}
