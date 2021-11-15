using Complejo.Application.Commands.User;
using Complejo.Application.Commands.User.Base;
using Complejo.Application.Interfaces.Repository;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Validators.User
{
    public class CreateClientUserValidator : AbstractValidator<CreateClientUserCommand>
    {
        private readonly IClientRepository clientRepository;

        public CreateClientUserValidator(IValidator<AbstractCreateUserCommand> validator, IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;

            Include(validator);

            RuleFor(x => x.IdClient)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El id es obligatorio.")
                .MustAsync(ExistClient)
                    .WithMessage("El cliente no existe. Imposible crear usuario.");
        }

        private async Task<bool> ExistClient(Guid id, CancellationToken cancellationToken)
        {
            return await clientRepository.Exist(id);
        }
    }
}
