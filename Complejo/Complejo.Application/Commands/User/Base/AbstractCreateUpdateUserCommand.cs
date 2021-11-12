namespace Complejo.Application.Commands.User.Base
{
    public abstract class AbstractCreateUpdateUserCommand
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RoleName { get; set; }
    }
}
