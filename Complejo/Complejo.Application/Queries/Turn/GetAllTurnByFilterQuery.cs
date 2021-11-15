using Complejo.Application.Dtos.Turn;
using Complejo.Application.Queries.Base;
using Complejo.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace Complejo.Application.Queries.Turn
{
    public class GetAllTurnByFilterQuery : GetAllPagedBaseQuery, IRequest<PagedListResponse<IList<TurnByFilterDto>>>
    {
        public DateTime? Date { get; set; }

        public string Time { get; set; }

        public Guid? IdFieldType { get; set; }

        public string ClientSearchCriteria { get; set; }
    }
}
