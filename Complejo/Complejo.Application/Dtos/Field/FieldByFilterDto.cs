using Complejo.Application.Dtos.Base;

namespace Complejo.Application.Dtos.Field
{
    public class FieldByFilterDto : DtoBase
    {
        public string Description { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
    }
}
