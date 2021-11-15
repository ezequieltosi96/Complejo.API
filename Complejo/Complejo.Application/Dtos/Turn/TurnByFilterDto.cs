using Complejo.Application.Dtos.Base;

namespace Complejo.Application.Dtos.Turn
{
    public class TurnByFilterDto : DtoBase
    {
        public string Code { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Field { get; set; }

        public string ClientName { get; set; }
    }
}
