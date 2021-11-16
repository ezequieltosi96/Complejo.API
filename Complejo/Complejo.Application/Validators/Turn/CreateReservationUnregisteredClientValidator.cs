using Complejo.Application.Commands.Turn;
using Complejo.Application.Interfaces.Repository;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Validators.Turn
{
    public class CreateReservationUnregisteredClientValidator : AbstractValidator<CreateReservationUnregisteredClientCommand>
    {
        private readonly IFieldRepository fieldRepository;


        public CreateReservationUnregisteredClientValidator(IFieldRepository fieldRepository)
        {
            this.fieldRepository = fieldRepository;

            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("La fecha debe ser igual o mayor a la fecha actual.");

            RuleFor(x => x.Time)
                .NotNull()
                .NotEmpty()
                    .WithMessage("La hora es obligatoria.");

            RuleFor(x => x.IdField)
                .MustAsync(ExistField).WithMessage("La cancha seleccionada no existe.");

            RuleFor(x => x.ClientDni)
                .NotNull()
                .MaximumLength(8)
                    .WithMessage("El DNI debe tener 8 dígitos.")
                .MaximumLength(8)
                    .WithMessage("El DNI debe tener 8 dígitos.")
                .NotEmpty()
                    .WithMessage("El DNI es obligatorio.");

            RuleFor(x => x.ClientPhoneNumber)
                .NotNull()
                .MaximumLength(13)
                    .WithMessage("El número de teléfono no debe superar los 13 dígitos.")
                .NotEmpty()
                    .WithMessage("El número de teléfono es obligatorio.");
        }

        private async Task<bool> ExistField(Guid id, CancellationToken cancellationToken)
        {
            return await fieldRepository.Exist(id);
        }
    }
}
