using Complejo.Application.Dtos.Turn;
using MediatR;

namespace Complejo.Application.Queries.Turn
{
    public class GetTurnByCodeQuery : IRequest<TurnByIdDto>
    {
        public string Code { get; set; }
    }
}
