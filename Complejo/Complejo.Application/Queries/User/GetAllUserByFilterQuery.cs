using Complejo.Application.Dtos.User;
using Complejo.Application.Queries.Base;
using Complejo.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace Complejo.Application.Queries.User
{
    public class GetAllUserByFilterQuery : GetAllPagedBaseQuery, IRequest<PagedListResponse<IList<UserByFilterDto>>>
    {
        public string SearchCriteria { get; set; }

    }
}
