using Complejo.Domain.Common;
using Complejo.Domain.Interfaces;

namespace Complejo.Domain.Entities
{
    public class FieldStatus : EntityBase, IDescription
    {
        public string Description { get; set; }

        public string DescriptionSearch { get; set; }

        public int IdFieldStatusGroup { get; set; }

        private enum FieldStatusGroup
        {
            AVAILABLE = 1,
            IN_MAITENANCE = 2,
            RESERVED = 3
        }

        public const int AVAILABLE = (int)FieldStatusGroup.AVAILABLE;
        public const int IN_MAITENANCE = (int)FieldStatusGroup.IN_MAITENANCE;
        public const int RESERVED = (int)FieldStatusGroup.RESERVED;
    }
}
