using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Entities
{
    public class AuditedEntityBase : BaseEntity, IAuditedEntity
    {
        /// <summary>
        /// Date the record was created
        /// </summary>        
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The username of the record creator
        /// </summary>
        [StringLength(50)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// The date the record was updated
        /// </summary>
        public DateTime? DateUpdated { get; set; }

        /// <summary>
        /// The username of the record updater
        /// </summary>
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public void Update()
        {
            this.DateUpdated = DateTime.Now;
        }
    }
}
