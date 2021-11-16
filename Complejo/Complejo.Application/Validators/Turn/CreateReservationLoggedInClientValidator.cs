using Complejo.Application.Commands.Turn;
using Complejo.Application.Interfaces.Repository;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Validators.Turn
{
    public class CreateReservationLoggedInClientValidator : AbstractValidator<CreateReservationLoggedInClientCommand>
    {
        private readonly IFieldRepository fieldRepository;
        private readonly IClientRepository clientRepository;

        public CreateReservationLoggedInClientValidator(IFieldRepository fieldRepository, IClientRepository clientRepository)
        {
            this.fieldRepository = fieldRepository;
            this.clientRepository = clientRepository;

            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("La fecha debe ser igual o mayor a la fecha actual.");

            RuleFor(x => x.Time)
                .NotNull()
                .NotEmpty()
                    .WithMessage("La hora es obligatoria.");

            RuleFor(x => x.IdField)
                .MustAsync(ExistField).WithMessage("La cancha seleccionada no existe.");

            RuleFor(x => x.IdClient)
                .MustAsync(ExistClient).WithMessage("La cliente no existe.");
        }

        private async Task<bool> ExistField(Guid id, CancellationToken cancellationToken)
        {
            return await fieldRepository.Exist(id);
        }

        private async Task<bool> ExistClient(Guid id, CancellationToken cancellationToken)
        {
            return await clientRepository.Exist(id);
        }
    }
}
