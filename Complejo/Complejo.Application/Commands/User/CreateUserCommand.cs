using Complejo.Application.Commands.User.Base;
using MediatR;

namespace Complejo.Application.Commands.User
{
    public class CreateUserCommand : AbstractCreateUpdateUserCommand, IRequest<string>
    {
    }
}
