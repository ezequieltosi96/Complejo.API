using MediatR;

namespace Complejo.Application.Commands.User
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public string IdUser { get; set; }
    }
}
