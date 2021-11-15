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
            var user = await userService.GetUserById(request.IdUser);

            if(user.IdClient.HasValue)
            {
                throw new Exceptions.BadRequestException("El usuario no se puede eliminar por que esta asignado a un cliente vigente.");
            }

            await userService.DeleteUser(request.IdUser);

            return true;
        }
    }
}
