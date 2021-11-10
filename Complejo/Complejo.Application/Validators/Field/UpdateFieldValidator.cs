using Complejo.Application.Commands.Field;
using Complejo.Application.Commands.Field.Base;
using FluentValidation;

namespace Complejo.Application.Validators.Field
{
    public class UpdateFieldValidator : AbstractValidator<UpdateFieldCommand>
    {
        public UpdateFieldValidator(IValidator<AbstractCreateUpdateFieldCommand> validator)
        {
            Include(validator);

            RuleFor(x => x.IdField)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El Id es obligatorio.");
        }
    }
}
