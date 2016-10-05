using DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Entities
{
    public class Pledge : AuditedEntityBase
    {
        [Precision(10,2)]
        public decimal Amount { get; set; }

        public DateTime DeadLine { get; set; }
    }
}
