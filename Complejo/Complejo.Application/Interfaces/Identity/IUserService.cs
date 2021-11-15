using Complejo.Application.Models.Identity.User;
using Complejo.Application.Responses;
using System;
using System.Threading.Tasks;

namespace Complejo.Application.Interfaces.Identity
{
    public interface IUserService
    {
        Task<PagedList<User>> GetAllAdminUserByFilter(string searchString = null, int page = 1, int size = 10);

        Task<User> GetUserById(string id);

        Task<string> CreateAdminUser(string email, string firstName, string lastName);

        Task<string> CreateAppUserUser(string email, string firstName, string lastName, Guid idClient);

        Task DeleteUser(string id);

        Task<User> GetByEmail(string email);

        Task ResetPassword(string idUser);
    }
}
