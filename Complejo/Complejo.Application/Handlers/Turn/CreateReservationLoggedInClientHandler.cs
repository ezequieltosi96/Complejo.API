using Complejo.Application.Commands.Turn;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Utils;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Turn
{
    public class CreateReservationLoggedInClientHandler : IRequestHandler<CreateReservationLoggedInClientCommand, string>
    {
        private readonly IValidator<CreateReservationLoggedInClientCommand> validator;

        private readonly ITurnRepository turnRepository;
        private readonly IMapping mapping;

        public CreateReservationLoggedInClientHandler(ITurnRepository turnRepository, IValidator<CreateReservationLoggedInClientCommand> validator, IMapping mapping)
        {
            this.turnRepository = turnRepository;
            this.validator = validator;
            this.mapping = mapping;
        }

        public async Task<string> Handle(CreateReservationLoggedInClientCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            TimeSpan time = new TimeSpan(int.Parse(request.Time), 0, 0);

            DateTime timeDate = request.Date.Date + time;

            var result = turnRepository.IsTurnAvailable(timeDate, request.IdField);
            if (result)
            {
                throw new Exceptions.BadRequestException("El turno no se encuentra disponible.");
            }

            Domain.Entities.Turn turn = mapping.Map<Domain.Entities.Turn>(request);

            turn.Time = timeDate;

            turn.Code = TurnCodeGenerator.GenerateCode();

            await turnRepository.AddAsync(turn);

            return turn.Code;
        }
    }
}
