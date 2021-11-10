using Complejo.Application.Commands.Field;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Field
{
    public class CreateFieldHandler : IRequestHandler<CreateFieldCommand, Guid>
    {
        private readonly IFieldRepository fieldRepository;
        private readonly IFieldStatusRepository fieldStatusRepository;
        private readonly IFieldTypeRepository fieldTypeRepository;
        private readonly IValidator<CreateFieldCommand> validator;
        private readonly IMapping mapping;

        public CreateFieldHandler(IFieldRepository fieldRepository, IMapping mapping, IValidator<CreateFieldCommand> validator, IFieldStatusRepository fieldStatusRepository, IFieldTypeRepository fieldTypeRepository)
        {
            this.fieldRepository = fieldRepository;
            this.mapping = mapping;
            this.validator = validator;
            this.fieldStatusRepository = fieldStatusRepository;
            this.fieldTypeRepository = fieldTypeRepository;
        }

        public async Task<Guid> Handle(CreateFieldCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            Domain.Entities.Field field = mapping.Map<Domain.Entities.Field>(request);

            await fieldRepository.AddAsync(field);

            return field.Id;
        }
    }
}
