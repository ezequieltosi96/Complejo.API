namespace Complejo.Application.Commands.User.Base
{
    public abstract class AbstractCreateUserCommand
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
