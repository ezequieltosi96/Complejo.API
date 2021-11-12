using Complejo.Application.Commands.User;
using Complejo.Application.Interfaces.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.User
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserService userService;

        public DeleteUserHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await userService.DeleteUser(request.IdUser);

            return true;
        }
    }
}
