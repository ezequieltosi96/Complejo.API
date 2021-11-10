using System;

namespace Complejo.Domain.Common
{
    public class EntityBase
    {
        public EntityBase()
        {
            this.Removed = false;
        }

        public Guid Id { get; set; }

        public bool Removed { get; set; }
    }
}
