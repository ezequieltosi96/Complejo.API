using Complejo.Application.Commands.Client;
using Complejo.Application.Commands.Turn;
using Complejo.Application.Commands.User;
using Complejo.Application.Interfaces.Identity;
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
    public class CreateReservationUnregisteredClientHandler : IRequestHandler<CreateReservationUnregisteredClientCommand, string>
    {
        private readonly IRequestHandler<CreateClientUserCommand, string> createUserHandler;
        private readonly IRequestHandler<CreateClientCommand, Guid> createClientHandler;

        private readonly IValidator<CreateReservationUnregisteredClientCommand> validator;

        private readonly ITurnRepository turnRepository;
        private readonly IClientRepository clientRepository;
        private readonly IUserService userService;
        private readonly IMapping mapping;

        public CreateReservationUnregisteredClientHandler(ITurnRepository turnRepository, IValidator<CreateReservationUnregisteredClientCommand> validator, IMapping mapping, IUserService userService, IRequestHandler<CreateClientUserCommand, string> createUserHandler, IClientRepository clientRepository, IRequestHandler<CreateClientCommand, Guid> createClientHandler)
        {
            this.turnRepository = turnRepository;
            this.validator = validator;
            this.mapping = mapping;
            this.userService = userService;
            this.createUserHandler = createUserHandler;
            this.clientRepository = clientRepository;
            this.createClientHandler = createClientHandler;
        }

        public async Task<string> Handle(CreateReservationUnregisteredClientCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            Guid? clientId;
            Models.Identity.User.User user = await userService.GetByEmail(request.ClientEmail);

            if (user != null)
            {
                clientId = user.IdClient;
            }
            else
            {
                var client = clientRepository.GetClientByDni(request.ClientDni);

                if (client != null)
                {
                    clientId = client.Id;
                }
                else
                {
                    clientId = await createClientHandler.Handle(new CreateClientCommand { Dni = request.ClientDni, PhoneNumber = request.ClientPhoneNumber, FirstName = request.ClientName, LastName = request.ClientLastName }, cancellationToken);
                }

                await createUserHandler.Handle(new CreateClientUserCommand { Email = request.ClientEmail, FirstName = request.ClientName, LastName = request.ClientLastName, IdClient = clientId.Value }, cancellationToken);
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
            turn.IdClient = clientId.Value;

            turn.Code = TurnCodeGenerator.GenerateCode();

            await turnRepository.AddAsync(turn);

            return turn.Code;
        }
    }
}
