using Complejo.Application.Commands.Field;
using Complejo.Application.Commands.Field.Base;
using FluentValidation;

namespace Complejo.Application.Validators.Field
{
    public class CreateFieldValidator : AbstractValidator<CreateFieldCommand>
    {
        public CreateFieldValidator(IValidator<AbstractCreateUpdateFieldCommand> validator)
        {
            Include(validator);
        }
    }
}
