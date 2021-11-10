using Complejo.Application.Commands.Field;
using Complejo.Application.Commands.Field.Base;
using Complejo.Application.Interfaces.Repository;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Validators.Field.Base
{
    public class CompositeCreateUpdateFieldValidator : AbstractValidator<AbstractCreateUpdateFieldCommand>
    {
        private readonly IFieldRepository fieldRepository;
        private readonly IFieldStatusRepository fieldStatusRepository;
        private readonly IFieldTypeRepository fieldTypeRepository;

        public CompositeCreateUpdateFieldValidator(IFieldRepository fieldRepository, IFieldStatusRepository fieldStatusRepository, IFieldTypeRepository fieldTypeRepository)
        {
            this.fieldRepository = fieldRepository;
            this.fieldStatusRepository = fieldStatusRepository;
            this.fieldTypeRepository = fieldTypeRepository;

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                    .WithMessage("La descripción es obligatoria.")
                .MaximumLength(100)
                    .WithMessage("La descripción no debe superar los 100 caracteres.")
                .MustAsync(async (model, description, cancellationToken) => 
                    {
                        if(model.GetType() ==  typeof(UpdateFieldCommand))
                        {
                            return await NotExistWithSameDescription(description, ((UpdateFieldCommand)model).IdField, cancellationToken);
                        }

                        return await NotExistWithSameDescription(description, null, cancellationToken);
                    })
                    .WithMessage("Existe una cancha con la misma descripción.");

            RuleFor(x => x.IdFieldStatus)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El estado es obligatorio.")
                .MustAsync(ExistFieldStatus)
                    .WithMessage("El estado no es valido.");

            RuleFor(x => x.IdFieldType)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El tipo es obligatorio.")
                .MustAsync(ExistFieldType)
                    .WithMessage("El tipo no es valido.");
        }

        private async Task<bool> ExistFieldStatus(Guid? id, CancellationToken cancellationToken)
        {
            return await fieldStatusRepository.Exist(id.Value);
        }

        private async Task<bool> ExistFieldType(Guid? id, CancellationToken cancellationToken)
        {
            return await fieldTypeRepository.Exist(id.Value);
        }

        private async Task<bool> NotExistWithSameDescription(string description, Guid? id, CancellationToken cancellationToken)
        {
            return !(await fieldRepository.ExistSameDescription(description, id));
        }
    }
}
