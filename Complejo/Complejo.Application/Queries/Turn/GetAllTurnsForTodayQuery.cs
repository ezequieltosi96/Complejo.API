using Complejo.Application.Dtos.Turn;
using Complejo.Application.Queries.Base;
using Complejo.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace Complejo.Application.Queries.Turn
{
    public class GetAllTurnsForTodayQuery : GetAllPagedBaseQuery, IRequest<PagedListResponse<IList<TurnByFilterDto>>>
    {
    }
}
