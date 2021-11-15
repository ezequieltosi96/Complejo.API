using Complejo.Application.Commands.User.Base;
using MediatR;

namespace Complejo.Application.Commands.User
{
    public class CreateAdminUserCommand : AbstractCreateUserCommand, IRequest<string>
    {
    }
}
