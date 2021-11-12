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
        }
    }
}
