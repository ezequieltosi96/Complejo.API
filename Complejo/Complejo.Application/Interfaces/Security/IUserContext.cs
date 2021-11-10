namespace Complejo.Application.Interfaces.Security
{
    public interface IUserContext
    {
        public string UserId { get; }

        public string UserName { get; }

        public string Email { get; }
    }
}
