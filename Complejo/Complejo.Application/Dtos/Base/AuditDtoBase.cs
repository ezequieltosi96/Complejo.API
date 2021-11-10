using System;

namespace Complejo.Application.Dtos.Base
{
    public class AuditDtoBase : DtoBase
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }
    }
}
