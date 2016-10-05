using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNI.Entities
{
    /// <summary>
    /// All audited items must comply
    /// </summary>
    public interface IAuditedEntity : IEntity
    {
        /// <summary>
        /// Date the record was created
        /// </summary>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// The username of the record creator
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// The date the record was updated
        /// </summary>
        DateTime? DateUpdated { get; set; }

        /// <summary>
        /// The username of the record updater
        /// </summary>
        string UpdatedBy { get; set; }
    }
}
