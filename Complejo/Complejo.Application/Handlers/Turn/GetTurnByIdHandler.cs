using Complejo.Application.Dtos.Turn;
using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Queries.Turn;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Turn
{
    public class GetTurnByIdHandler : IRequestHandler<GetTurnByIdQuery, TurnByIdDto>
    {
        private readonly ITurnRepository turnRepository;
        private readonly IMapping mapping;

        public GetTurnByIdHandler(ITurnRepository turnRepository, IMapping mapping)
        {
            this.turnRepository = turnRepository;
            this.mapping = mapping;
        }

        public async Task<TurnByIdDto> Handle(GetTurnByIdQuery request, CancellationToken cancellationToken)
        {
            var turn = await turnRepository.GetByIdAsync(request.IdTurn.Value);

            if(turn == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Turn), request.IdTurn);
            }

            return mapping.Map<TurnByIdDto>(turn);
        }
    }
}
