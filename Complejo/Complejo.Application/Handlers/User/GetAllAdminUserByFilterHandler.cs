using Complejo.Application.Dtos.User;
using Complejo.Application.Interfaces.Identity;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Queries.User;
using Complejo.Application.Responses;
using Complejo.Application.Utils;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.User
{
    public class GetAllAdminUserByFilterHandler : IRequestHandler<GetAllAdminUserByFilterQuery, PagedListResponse<IList<UserByFilterDto>>>
    {
        private readonly IUserService userService;
        private readonly IMapping mapping;

        public GetAllAdminUserByFilterHandler(IUserService userService, IMapping mapping)
        {
            this.userService = userService;
            this.mapping = mapping;
        }

        public async Task<PagedListResponse<IList<UserByFilterDto>>> Handle(GetAllAdminUserByFilterQuery request, CancellationToken cancellationToken)
        {
            PagedList<Models.Identity.User.User> users = await userService.GetAllAdminUserByFilter(request.SearchCriteria, request.Page, request.Size);

            IList<UserByFilterDto> dtos = mapping.Map<IList<UserByFilterDto>>(users);

            Metadata metadata = new Metadata
            {
                TotalCount = users.TotalCount,
                TotalPages = users.TotalPages,
                CurrentPage = users.CurrentPage,
                PageSize = users.PageSize,
                HasNextPage = users.HasNextPage,
                HasPreviousPage = users.HasPreviousPage,
                NextPageNumber = users.NextPageNumber,
                PreviousPageNumber = users.PreviousPageNumber,
            };

            return new PagedListResponse<IList<UserByFilterDto>>(dtos, metadata);
        }
    }
}
