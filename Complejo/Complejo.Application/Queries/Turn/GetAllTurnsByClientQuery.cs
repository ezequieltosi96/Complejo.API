using Complejo.Application.Dtos.Turn;
using Complejo.Application.Queries.Base;
using Complejo.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace Complejo.Application.Queries.Turn
{
    public class GetAllTurnsByClientQuery : GetAllPagedBaseQuery, IRequest<PagedListResponse<IList<TurnByClientDto>>>
    {
        public Guid? IdClient { get; set; }
    }
}
