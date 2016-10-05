using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Entities
{
    public class PaymentConfiguration : AuditedEntityBase
    {
        [StringLength(500)]
        public string Secret { get; set; }
    }
}
