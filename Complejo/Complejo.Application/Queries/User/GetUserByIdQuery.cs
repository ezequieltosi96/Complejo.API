using Complejo.Application.Dtos.User;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Queries.User
{
    public class GetUserByIdQuery : IRequest<UserByIdDto>
    {
        [Required]
        public string IdUser { get; set; }
    }
}
