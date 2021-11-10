using Complejo.Application.Commands.Field;
using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Field
{
    public class UpdateFieldHandler : IRequestHandler<UpdateFieldCommand, Guid>
    {
        private readonly IFieldRepository fieldRepository;
        private readonly IMapping mapping;
        private readonly IValidator<UpdateFieldCommand> validator;

        public UpdateFieldHandler(IFieldRepository fieldRepository, IMapping mapping, IValidator<UpdateFieldCommand> validator)
        {
            this.fieldRepository = fieldRepository;
            this.mapping = mapping;
            this.validator = validator;
        }

        public async Task<Guid> Handle(UpdateFieldCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Field field = await fieldRepository.GetByIdAsync(request.IdField.Value);

            if(field == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Field), request.IdField);
            }

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            mapping.Map<UpdateFieldCommand, Domain.Entities.Field>(request, field);

            await fieldRepository.UpdateAsync(field);

            return field.Id;
        }
    }
}
