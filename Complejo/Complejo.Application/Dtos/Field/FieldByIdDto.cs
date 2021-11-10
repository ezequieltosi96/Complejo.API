using Complejo.Application.Dtos.Base;
using System;

namespace Complejo.Application.Dtos.Field
{
    public class FieldByIdDto : DtoBase
    {
        public string Description { get; set; }

        public int IdStatusGroup { get; set; }

        public Guid IdStatus { get; set; }

        public string Status { get; set; }

        public int IdTypeGroup { get; set; }

        public Guid IdType { get; set; }

        public string Type { get; set; }

    }
}
