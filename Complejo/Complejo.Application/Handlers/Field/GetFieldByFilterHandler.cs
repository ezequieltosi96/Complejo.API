using Complejo.Application.Dtos.Field;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Queries.Field;
using Complejo.Application.Responses;
using Complejo.Application.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Field
{
    public class GetFieldByFilterHandler : IRequestHandler<GetFieldByFilterQuery, PagedListResponse<IList<FieldByFilterDto>>>
    {
        private readonly IFieldRepository fieldRepository;
        private readonly IMapping mapping;

        public GetFieldByFilterHandler(IFieldRepository fieldRepository, IMapping mapping)
        {
            this.fieldRepository = fieldRepository;
            this.mapping = mapping;
        }

        public async Task<PagedListResponse<IList<FieldByFilterDto>>> Handle(GetFieldByFilterQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entities.Field, bool>> predicate = x => request.Description != null ? x.DescriptionSearch.Contains(request.Description.Trim().ToUpper()) : true && 
                                                                           request.IdFieldStatus.HasValue ? x.FieldStatus.Id == request.IdFieldStatus.Value : true &&
                                                                           request.IdFieldType.HasValue ? x.FieldType.Id == request.IdFieldStatus.Value : true;

            Func<IQueryable<Domain.Entities.Field>, IOrderedQueryable<Domain.Entities.Field>> order = x => x.OrderBy(f => f.Description);

            PagedList<Domain.Entities.Field> fields = await fieldRepository.GetAllByFilter(predicate, order, x => x.Include(field => field.FieldStatus).Include(field => field.FieldType), false, true, request.Page, request.Size);

            IList<FieldByFilterDto> dtos = mapping.Map<IList<FieldByFilterDto>>(fields);

            Metadata metadata = new Metadata
            {
                TotalCount = fields.TotalCount,
                TotalPages = fields.TotalPages,
                CurrentPage = fields.CurrentPage,
                PageSize = fields.PageSize,
                HasNextPage = fields.HasNextPage,
                HasPreviousPage = fields.HasPreviousPage,
                NextPageNumber = fields.NextPageNumber,
                PreviousPageNumber = fields.PreviousPageNumber,
            };

            return new PagedListResponse<IList<FieldByFilterDto>>(dtos, metadata);
        }
    }
}
