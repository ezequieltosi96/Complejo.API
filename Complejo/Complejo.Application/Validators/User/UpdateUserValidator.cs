using Complejo.Application.Commands.User;
using Complejo.Application.Commands.User.Base;
using FluentValidation;

namespace Complejo.Application.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator(IValidator<AbstractCreateUpdateUserCommand> validator)
        {
            Include(validator);

            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El id es obligatorio.");
        }
    }
}
