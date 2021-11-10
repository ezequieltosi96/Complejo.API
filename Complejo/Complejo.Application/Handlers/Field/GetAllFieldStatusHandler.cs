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
    public class GetAllFieldStatusHandler : IRequestHandler<GetAllFieldStatusQuery, IEnumerable<ComboBoxDto>>
    {
        private readonly IFieldStatusRepository fieldStatusRepository;
        private readonly IFieldRepository fieldRepository;
        private readonly IMapping mapping;

        public GetAllFieldStatusHandler(IFieldStatusRepository fieldStatusRepository, IFieldRepository fieldRepository, IMapping mapping)
        {
            this.fieldStatusRepository = fieldStatusRepository;
            this.fieldRepository = fieldRepository;
            this.mapping = mapping;
        }

        public async Task<IEnumerable<ComboBoxDto>> Handle(GetAllFieldStatusQuery request, CancellationToken cancellationToken)
        {
            IList<Domain.Entities.FieldStatus> fieldStatuses = await fieldStatusRepository.ListAllAsync();

            if(request.IdEntity.HasValue)
            {
                Domain.Entities.Field field = await fieldRepository.GetByIdAsync(request.IdEntity.Value, x => x.Include(field => field.FieldStatus));

                if(field != null && !fieldStatuses.Any(x => x.Id == field.FieldStatus.Id))
                {
                    fieldStatuses.Add(field.FieldStatus);
                }
            }

            return mapping.Map<IEnumerable<ComboBoxDto>>(fieldStatuses).OrderBy(x => x.Name);
        }
    }
}
