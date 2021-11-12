using Complejo.Application.Commands.User.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Commands.User
{
    public class CreateUserCommand : AbstractCreateUpdateUserCommand, IRequest<string>
    {
        [Required]
        public string RoleName { get; set; }
    }
}
