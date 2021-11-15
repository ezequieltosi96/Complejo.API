using Complejo.Application.Dtos.Base;

namespace Complejo.Application.Dtos.Turn
{
    public class TurnByClientDto : DtoBase
    {
        public string Code { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Field { get; set; }

        public string FieldType { get; set; }
    }
}
