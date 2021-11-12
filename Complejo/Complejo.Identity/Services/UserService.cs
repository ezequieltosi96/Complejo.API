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
            this.dbContext = dbContext;
            this.roleManager = roleManager;
        }

        public async Task<PagedList<User>> GetAllUserByFilter(string searchString = null, int page = 1, int size = 10)
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
                var user = new User();
                user.Id = appUser.Id;
                user.LastName = appUser.LastName;
                user.FirstName = appUser.FirstName;
                user.Email = appUser.Email;
                user.UserName = appUser.UserName;
                user.RoleName = (await userManager.GetRolesAsync(appUser)).FirstOrDefault();

                users.Add(user);
            }

            return new PagedList<User>(users, count, page, size);
        }

        public async Task<string> CreateUser(string email, string firstName, string lastName, string role)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new BadRequestException("Ya existe un usuario registrado con ese correo.");
            }

            var roleExist = await roleManager.RoleExistsAsync(role);

            if(!roleExist)
            {
                throw new NotFoundException("Role", role);
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
                await userManager.AddToRoleAsync(user, role);
                return user.Id;
            }

            return null;
        }

        public async Task<string> UpdateUser(string id, string email, string firstName, string lastName, string role)
        {
            var existingUser = await userManager.FindByIdAsync(id);

            if (existingUser == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            var existingUserWithSameEmail = await userManager.FindByEmailAsync(email);

            if (existingUserWithSameEmail != null && existingUser.Id != id)
            {
                throw new BadRequestException("Ya existe un usuario registrado con ese correo.");
            }

            var counter = new Counter { Count = 0 };
            await CountRepeatedUserName($"{firstName.ToLower()}.{lastName.ToLower()}", id, counter);

            existingUser.Email = email;
            existingUser.FirstName = firstName;
            existingUser.LastName = lastName;
            existingUser.UserName = counter.Count > 0 ? $"{firstName.ToLower()}.{lastName.ToLower()}{counter.Count}" : $"{firstName.ToLower()}.{lastName.ToLower()}";

            var oldRole = (await userManager.GetRolesAsync(existingUser)).FirstOrDefault();

            var a = await userManager.AddToRoleAsync(existingUser, role);
            var b = await userManager.RemoveFromRoleAsync(existingUser, oldRole);
            await userManager.UpdateAsync(existingUser);

            return existingUser.Id;
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

            var user = new User();
            user.Id = existingUser.Id;
            user.LastName = existingUser.LastName;
            user.FirstName = existingUser.FirstName;
            user.Email = existingUser.Email;
            user.UserName = existingUser.UserName;
            user.RoleName = (await userManager.GetRolesAsync(existingUser)).FirstOrDefault();

            return user;
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
