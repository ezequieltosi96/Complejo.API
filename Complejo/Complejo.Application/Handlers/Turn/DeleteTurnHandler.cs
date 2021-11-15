using Complejo.Application.Commands.Turn;
using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Turn
{
    public class DeleteTurnHandler : IRequestHandler<DeleteTurnCommand, bool>
    {
        private readonly ITurnRepository turnRepository;

        public DeleteTurnHandler(ITurnRepository turnRepository)
        {
            this.turnRepository = turnRepository;
        }

        public async Task<bool> Handle(DeleteTurnCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Turn turn = await turnRepository.GetByIdAsync(request.IdTurn.Value);

            if (turn == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Turn), request.IdTurn);
            }

            await turnRepository.DeleteAsync(turn);

            return true;
        }
    }
}
