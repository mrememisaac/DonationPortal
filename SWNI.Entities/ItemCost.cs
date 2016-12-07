using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Entities
{
    public class ItemCost : AuditedEntityBase
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
