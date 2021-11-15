using Complejo.Domain.Common;

namespace Complejo.Domain.Entities
{
    public class Client : EntityBase
    {
        public string FullName { get; set; }

        public string FullNameSearch { get; set; }

        public string PhoneNumber { get; set; }

        public string Dni { get; set; }
    }
}
