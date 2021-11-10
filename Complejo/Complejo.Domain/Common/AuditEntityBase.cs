using System;

namespace Complejo.Domain.Common
{
    public class AuditEntityBase : EntityBase
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }
    }
}
