using Complejo.Application.Dtos.Field;
using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Queries.Field;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Field
{
    public class GetFieldByIdHandler : IRequestHandler<GetFieldByIdQuery, FieldByIdDto>
    {
        private readonly IFieldRepository fieldRepository;
        private readonly IMapping mapping;

        public GetFieldByIdHandler(IFieldRepository fieldRepository, IMapping mapping)
        {
            this.fieldRepository = fieldRepository;
            this.mapping = mapping;
        }

        public async Task<FieldByIdDto> Handle(GetFieldByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Field field = await fieldRepository.GetByIdAsync(request.IdField.Value, x => x.Include(field => field.FieldStatus).Include(field => field.FieldType));

            if (field == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Field), request.IdField);
            }

            return mapping.Map<FieldByIdDto>(field);
        }
    }
}
