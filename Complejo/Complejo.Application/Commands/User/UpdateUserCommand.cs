using Complejo.Application.Commands.User.Base;
using MediatR;

namespace Complejo.Application.Commands.User
{
    public class UpdateUserCommand : AbstractCreateUpdateUserCommand, IRequest<string>
    {
        public string Id { get; set; }
    }
}
