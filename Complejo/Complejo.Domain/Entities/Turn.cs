using Complejo.Domain.Common;
using System;

namespace Complejo.Domain.Entities
{
    public class Turn : AuditEntityBase
    {
        public string Code { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public Guid IdField { get; set; }

        public Guid IdClient { get; set; }

        public virtual Field Field { get; set; }

        public virtual Client Client { get; set; }
    }
}
