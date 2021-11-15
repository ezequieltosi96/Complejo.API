using Complejo.Domain.Common;
using Complejo.Domain.Interfaces;

namespace Complejo.Domain.Entities
{
    public class TurnStatus : EntityBase, IDescription
    {
        public string Description { get; set; }

        public string DescriptionSearch { get; set; }

        public int IdTurnStatusGroup { get; set; }

        private enum TurnStatusGroup
        {
            AVAILABLE = 1,
            RESERVED = 2,
        }

        public const int AVAILABLE = (int)TurnStatusGroup.AVAILABLE;
        public const int RESERVED = (int)TurnStatusGroup.RESERVED;
    }
}
