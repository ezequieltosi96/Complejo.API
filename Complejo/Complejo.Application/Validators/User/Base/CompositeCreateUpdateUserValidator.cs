using Complejo.Application.Commands.User.Base;
using Complejo.Application.Interfaces.Identity;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Validators.User.Base
{
    public class CompositeCreateUpdateUserValidator : AbstractValidator<AbstractCreateUserCommand>
    {
        private readonly IUserService userService;

        public CompositeCreateUpdateUserValidator(IUserService userService)
        {
            this.userService = userService;
        
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El correo es obligatorio.")
                .EmailAddress()
                    .WithMessage("Debe ingresar un email valido.")
                 .MustAsync(ValidEmail)
                    .WithMessage("El correo ya se encuentra registrado.");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El nombre es obligatorio.");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El apellido es obligatorio.");
        }

        private async Task<bool> ValidEmail(string email, CancellationToken cancellationToken)
        {
            return (await userService.GetByEmail(email)) == null;
        }
    }
}
