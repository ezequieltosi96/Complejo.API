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
        private readonly IValidator<CreateFieldCommand> validator;
        private readonly IMapping mapping;

        public CreateFieldHandler(IFieldRepository fieldRepository, IMapping mapping, IValidator<CreateFieldCommand> validator)
        {
            this.fieldRepository = fieldRepository;
            this.mapping = mapping;
            this.validator = validator;
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
