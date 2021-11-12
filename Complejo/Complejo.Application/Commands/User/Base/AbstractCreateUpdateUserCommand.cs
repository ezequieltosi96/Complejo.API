using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Commands.User.Base
{
    public abstract class AbstractCreateUpdateUserCommand
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
