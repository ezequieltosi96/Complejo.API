using Complejo.Application.Commands.User;
using Complejo.Application.Commands.User.Base;
using FluentValidation;

namespace Complejo.Application.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator(IValidator<AbstractCreateUpdateUserCommand> validator)
        {
            Include(validator);

            RuleFor(x => x.RoleName)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El rol es obligatorio.")
                .Must(ExistRole)
                    .WithMessage("El rol no es valido.");
        }

        private bool ExistRole(string role)
        {
            if(role != "Admin" && role != "AppUser")
            {
                return false;
            }

            return true;
        }
    }
}
