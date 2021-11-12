using Complejo.Application.Dtos.Field;
using Complejo.Application.Queries.Base;
using Complejo.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace Complejo.Application.Queries.Field
{
    public class GetAllFieldByFilterQuery : GetAllPagedBaseQuery, IRequest<PagedListResponse<IList<FieldByFilterDto>>>
    {
        public Guid? IdFieldType { get; set; }

        public Guid? IdFieldStatus { get; set; }

        public string Description { get; set; }

    }
}
