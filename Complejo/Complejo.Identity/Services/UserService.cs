using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Identity;
using Complejo.Application.Models.Identity.User;
using Complejo.Application.Responses;
using Complejo.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Complejo.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationIdentityDbContext dbContext;

        public UserService(UserManager<ApplicationUser> userManager, ApplicationIdentityDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
        }

        public async Task<PagedList<User>> GetAllAdminUserByFilter(string searchString = null, int page = 1, int size = 10)
        {
            IQueryable<ApplicationUser> query = dbContext.Users;

            int count = query.Count();

            if (searchString != null)
            {
                query = query.Where(x => x.NormalizedEmail.Contains(searchString.ToUpper()) || 
                                         x.FirstName.ToUpper().Contains(searchString.ToUpper()) ||
                                         x.LastName.ToUpper().Contains(searchString.ToUpper()) ||
                                         x.UserName.ToUpper().Contains(searchString.ToUpper()));
            }

            IList<ApplicationUser> applicationUsers = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            IList<User> users = new List<User>();

            foreach (var appUser in applicationUsers)
            {
                var roleName = (await userManager.GetRolesAsync(appUser)).FirstOrDefault();
                if (roleName == "Admin")
                {
                    var user = new User
                    {
                        Id = appUser.Id,
                        LastName = appUser.LastName,
                        FirstName = appUser.FirstName,
                        Email = appUser.Email,
                        UserName = appUser.UserName,
                        RoleName = roleName,
                        IdClient = appUser.IdClient
                    };

                    users.Add(user);
                }
            }

            return new PagedList<User>(users, count, page, size);
        }

        public async Task<string> CreateAdminUser(string email, string firstName, string lastName)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new BadRequestException("Ya existe un usuario registrado con ese correo.");
            }

            var counter = new Counter { Count = 0 };
            await CountRepeatedUserName($"{firstName.ToLower()}.{lastName.ToLower()}", counter);

            var user = new ApplicationUser()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserName = counter.Count > 0 ? $"{firstName.ToLower()}.{lastName.ToLower()}{counter.Count}" : $"{firstName.ToLower()}.{lastName.ToLower()}",
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, $"{firstName.ToLower()}@{lastName.ToLower()}");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                return user.Id;
            }

            return null;
        }

        public async Task<string> CreateAppUserUser(string email, string firstName, string lastName, System.Guid idClient)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new BadRequestException("Ya existe un usuario registrado con ese correo.");
            }

            var counter = new Counter { Count = 0 };
            await CountRepeatedUserName($"{firstName.ToLower()}.{lastName.ToLower()}", counter);

            var user = new ApplicationUser()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserName = counter.Count > 0 ? $"{firstName.ToLower()}.{lastName.ToLower()}{counter.Count}" : $"{firstName.ToLower()}.{lastName.ToLower()}",
                EmailConfirmed = true,
                IdClient = idClient
            };

            var result = await userManager.CreateAsync(user, $"{firstName.ToLower()}@{lastName.ToLower()}");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "AppUser");
                return user.Id;
            }

            return null;
        }

        public async Task DeleteUser(string id)
        {
            var existingUser = await userManager.FindByIdAsync(id);

            if (existingUser == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            await userManager.DeleteAsync(existingUser);
        }

        public async Task<User> GetUserById(string id)
        {
            var existingUser = await userManager.FindByIdAsync(id);

            if (existingUser == null)
            {
                return null;
            }

            var user = new User
            {
                Id = existingUser.Id,
                LastName = existingUser.LastName,
                FirstName = existingUser.FirstName,
                Email = existingUser.Email,
                UserName = existingUser.UserName,
                RoleName = (await userManager.GetRolesAsync(existingUser)).FirstOrDefault(),
                IdClient = existingUser.IdClient
            };

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                return null;
            }

            var user = new User
            {
                Id = existingUser.Id,
                LastName = existingUser.LastName,
                FirstName = existingUser.FirstName,
                Email = existingUser.Email,
                UserName = existingUser.UserName,
                RoleName = (await userManager.GetRolesAsync(existingUser)).FirstOrDefault(),
                IdClient = existingUser.IdClient
            };

            return user;
        }

        public async Task ResetPassword(string idUser)
        {
            var user = await userManager.FindByIdAsync(idUser);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            await userManager.ResetPasswordAsync(user, token, $"{user.FirstName.ToLower()}@{user.LastName.ToLower()}");
        }

        private async Task CountRepeatedUserName(string username, string id, Counter counter)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user != null && user.Id != id)
            {
                counter.Count++;
                await CountRepeatedUserName($"{username}{counter.Count}", id, counter);
            }
        }

        private async Task CountRepeatedUserName(string username, Counter counter)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user != null)
            {
                counter.Count++;
                await CountRepeatedUserName($"{username}{counter.Count}", counter);
            }
        }
    }

    public class Counter
    {
        public int Count { get; set; }
    }
}
