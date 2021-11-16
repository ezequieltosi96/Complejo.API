using Complejo.Application.Commands.Turn;
using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Turn
{
    public class CancelTurnHandler : IRequestHandler<CancelTurnCommand, bool>
    {
        private readonly ITurnRepository turnRepository;

        public CancelTurnHandler(ITurnRepository turnRepository)
        {
            this.turnRepository = turnRepository;
        }

        public async Task<bool> Handle(CancelTurnCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Turn turn = await turnRepository.GetByIdAsync(request.Id);

            if (turn == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Turn), request.Id);
            }

            if(turn.Date.Date < DateTime.Today.Date)
            {
                throw new BadRequestException("El turno no puede cancelarse por que ya expiró");
            }

            await turnRepository.DeleteAsync(turn);

            return true;
        }
    }
}
