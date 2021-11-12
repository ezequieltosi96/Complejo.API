using Complejo.Application.Models.Identity.User;
using Complejo.Application.Responses;
using System.Threading.Tasks;

namespace Complejo.Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<PagedList<User>> GetAllUserByFilter(string searchString = null, int page = 1, int size = 10);

        Task<string> CreateUser(string email, string firstName, string lastName, string role);

        Task<string> UpdateUser(string id, string email, string firstName, string lastName);

        Task DeleteUser(string id);
    }
}
