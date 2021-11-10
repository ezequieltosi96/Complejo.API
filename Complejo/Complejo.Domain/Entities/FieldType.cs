using Complejo.Domain.Common;
using Complejo.Domain.Interfaces;

namespace Complejo.Domain.Entities
{
    public class FieldType : EntityBase, IDescription
    {
        public string Description { get; set; }

        public string DescriptionSearch { get; set; }

        public int IdFieldTypeGroup { get; set; }

        private enum FieldTypeGroup
        {
            FIVE = 1,
            EIGHT = 2,
            ELEVEN = 3
        }

        public const int FIVE = (int)FieldTypeGroup.FIVE;
        public const int EIGHT = (int)FieldTypeGroup.EIGHT;
        public const int ELEVEN = (int)FieldTypeGroup.ELEVEN;
    }
}
