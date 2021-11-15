using Complejo.Application.Commands.Client;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Client
{
    public class CreateClientHandler : IRequestHandler<CreateClientCommand, Guid>
    {
        private readonly IClientRepository clientRepository;
        private readonly IValidator<CreateClientCommand> validator;
        private readonly IMapping mapping;

        public CreateClientHandler(IClientRepository clientRepository, IValidator<CreateClientCommand> validator, IMapping mapping)
        {
            this.clientRepository = clientRepository;
            this.validator = validator;
            this.mapping = mapping;
        }

        public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            Domain.Entities.Client client = mapping.Map<Domain.Entities.Client>(request);

            await clientRepository.AddAsync(client);

            return client.Id;
        }
    }
}
