using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Queries.Turn;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Validators.Turn
{
    public class GetAllTurnByClientValidator : AbstractValidator<GetAllTurnsByClientQuery>
    {
        private readonly IClientRepository clientRepository;

        public GetAllTurnByClientValidator(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;

            RuleFor(x => x.IdClient)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El id es obligatorio.")
                .MustAsync(ExistClient)
                    .WithMessage("El cliente no existe. Imposible crear usuario.");
        }

        private async Task<bool> ExistClient(Guid? id, CancellationToken cancellationToken)
        {
            return await clientRepository.Exist(id.Value);
        }
    }
}
