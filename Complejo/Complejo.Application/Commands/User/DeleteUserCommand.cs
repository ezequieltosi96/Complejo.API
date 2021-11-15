using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Commands.User
{
    public class DeleteUserCommand : IRequest<bool>
    {
        [Required]
        public string IdUser { get; set; }
    }
}
