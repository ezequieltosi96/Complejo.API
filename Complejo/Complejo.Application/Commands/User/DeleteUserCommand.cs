using MediatR;

namespace Complejo.Application.Commands.User
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string IdUser { get; set; }
    }
}
