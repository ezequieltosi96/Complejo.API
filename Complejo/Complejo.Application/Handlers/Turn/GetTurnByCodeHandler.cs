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
    public class GetTurnByCodeHandler : IRequestHandler<GetTurnByCodeQuery, TurnByIdDto>
    {
        private readonly ITurnRepository turnRepository;
        private readonly IMapping mapping;

        public GetTurnByCodeHandler(ITurnRepository turnRepository, IMapping mapping)
        {
            this.turnRepository = turnRepository;
            this.mapping = mapping;
        }

        public async Task<TurnByIdDto> Handle(GetTurnByCodeQuery request, CancellationToken cancellationToken)
        {
            var turn = await turnRepository.GetByCode(request.Code);

            if(turn == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Turn), request.Code);
            }

            return mapping.Map<TurnByIdDto>(turn);
        }
    }
}
