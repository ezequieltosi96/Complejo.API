using Complejo.Application.Dtos.User;
using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Identity;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Queries.User;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.User
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserByIdDto>
    {
        private readonly IUserService userService;
        private readonly IMapping mapping;

        public GetUserByIdHandler(IUserService userService, IMapping mapping)
        {
            this.userService = userService;
            this.mapping = mapping;
        }

        public async Task<UserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            Models.Identity.User.User user = await userService.GetUserById(request.IdUser);

            if(user == null)
            {
                throw new NotFoundException(nameof(User), request.IdUser);
            }

            return mapping.Map<UserByIdDto>(user);
        }
    }
}
