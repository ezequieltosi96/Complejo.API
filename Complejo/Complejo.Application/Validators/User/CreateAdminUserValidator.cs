using Complejo.Application.Commands.User;
using Complejo.Application.Commands.User.Base;
using FluentValidation;

namespace Complejo.Application.Validators.User
{
    public class CreateAdminUserValidator : AbstractValidator<CreateAdminUserCommand>
    {
        public CreateAdminUserValidator(IValidator<AbstractCreateUserCommand> validator)
        {
            Include(validator);
        }
    }
}
