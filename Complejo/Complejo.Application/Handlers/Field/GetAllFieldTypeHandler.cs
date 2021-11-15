using Complejo.Application.Dtos.UiControls;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Queries.Field;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Field
{
    public class GetAllFieldTypeHandler : IRequestHandler<GetAllFieldTypeQuery, IEnumerable<ComboBoxDto>>
    {
        private readonly IFieldTypeRepository fieldTypeRepository;
        private readonly IFieldRepository fieldRepository;
        private readonly IMapping mapping;

        public GetAllFieldTypeHandler(IMapping mapping, IFieldRepository fieldRepository, IFieldTypeRepository fieldTypeRepository)
        {
            this.mapping = mapping;
            this.fieldRepository = fieldRepository;
            this.fieldTypeRepository = fieldTypeRepository;
        }

        public async Task<IEnumerable<ComboBoxDto>> Handle(GetAllFieldTypeQuery request, CancellationToken cancellationToken)
        {
            IList<Domain.Entities.FieldType> fieldTypes = await fieldTypeRepository.ListAllAsync();

            if (request.IdEntity.HasValue)
            {
                Domain.Entities.Field field = await fieldRepository.GetByIdAsync(request.IdEntity.Value, x => x.Include(field => field.FieldType));

                if (field != null && !fieldTypes.Any(x => x.Id == field.FieldType.Id))
                {
                    fieldTypes.Add(field.FieldType);
                }
            }

            return mapping.Map<IEnumerable<ComboBoxDto>>(fieldTypes.OrderBy(x => x.IdFieldTypeGroup));
        }
    }
}
