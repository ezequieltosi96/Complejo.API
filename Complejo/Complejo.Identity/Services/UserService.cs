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

            IList<User> users = applicationUsers.Select(x => new User() {
                Id = x.Id,
                UserName = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }).ToList();

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

            var user = new ApplicationUser()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserName = $"{firstName}.{lastName}",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, $"{firstName}@{lastName}");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                return user.Id;
            }

            return null;
        }

        public async Task<string> UpdateUser(string id, string email, string firstName, string lastName)
        {
            var existingUser = await userManager.FindByIdAsync(id);

            if (existingUser == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            var existingUserWithSameEmail = await userManager.FindByEmailAsync(email);

            if (existingUserWithSameEmail != null)
            {
                throw new BadRequestException("Ya existe un usuario registrado con ese correo.");
            }

            existingUser.Email = email;
            existingUser.FirstName = firstName;
            existingUser.LastName = lastName;
            existingUser.UserName = $"{firstName}.{lastName}";

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
    }
}
