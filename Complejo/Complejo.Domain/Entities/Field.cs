using Complejo.Domain.Common;
using Complejo.Domain.Interfaces;
using System;

namespace Complejo.Domain.Entities
{
    public class Field : AuditEntityBase, IDescription
    {
        public string Description { get; set; }

        public string DescriptionSearch { get; set; }

        public Guid IdFieldStatus { get; set; }

        public Guid IdFieldType { get; set; }

        public virtual FieldStatus FieldStatus { get; set; }

        public virtual FieldType FieldType { get; set; }
    }
}
