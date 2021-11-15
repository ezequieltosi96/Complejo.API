using Complejo.Application.Commands.Client;
using Complejo.Application.Interfaces.Repository;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Validators.Client
{
    public class CreateClientValidator : AbstractValidator<CreateClientCommand>
    {
        private readonly IClientRepository clientRepository;

        public CreateClientValidator(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;

            RuleFor(x => x.Dni)
                .NotNull()
                .MaximumLength(8)
                    .WithMessage("El DNI debe tener 8 dígitos.")
                .MaximumLength(8)
                    .WithMessage("El DNI debe tener 8 dígitos.")
                .NotEmpty()
                    .WithMessage("El DNI es obligatorio.")
                .Must(NotExistWithSameDni)
                    .WithMessage("El DNI no es valido. Ya se encuentra registrado un cliente con el mismo DNI.");

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .MaximumLength(13)
                    .WithMessage("El número de telefono no debe superar los 13 dígitos.")
                .NotEmpty()
                    .WithMessage("El número de telefono es obligatorio.");
        }

        private bool NotExistWithSameDni(string dni)
        {
            return clientRepository.GetClientByDni(dni) == null;
        }
    }
}
