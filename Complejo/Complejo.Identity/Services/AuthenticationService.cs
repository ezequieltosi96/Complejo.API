using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Identity;
using Complejo.Application.Models.Identity.Authentication;
using Complejo.Application.Models.Identity.Jwt;
using Complejo.Application.Models.Identity.Registration;
using Complejo.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Complejo.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSettings jwtSettings;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings.Value;
            this.roleManager = roleManager;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"User with '{request.Email}' not found.");
            }

            var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception($"Credentials for '{request.Email}' aren't valid.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            AuthenticationResponse response = new AuthenticationResponse()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
            };

            return response;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            var roleResult = await ManageRole(request.RoleName);
            if(!roleResult)
            {
                throw new BadRequestException("An error has occured while register the user.");
            }

            var counter = new Counter { Count = 0 };
            await CountRepeatedUserName($"{request.FirstName.ToLower()}.{request.LastName.ToLower()}", counter);

            var user = new ApplicationUser()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = counter.Count > 0 ? $"{request.FirstName.ToLower()}.{request.LastName.ToLower()}{counter.Count}" : $"{request.FirstName.ToLower()}.{request.LastName.ToLower()}",
                EmailConfirmed = true,
                IdClient = request.IdClient.Value,
            };

            var existingEmail = await userManager.FindByEmailAsync(request.Email);

            if (existingEmail != null)
            {
                throw new Exception($"Email '{request.Email} already exists.");
            }

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, request.RoleName);
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);

            await userManager.RemoveClaimsAsync(user, userClaims);

            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("isAdmin", roles.Contains("Admin").ToString()),
                new Claim("idClient", user.IdClient.HasValue ? user.IdClient.ToString() : "")
            }.Union(roleClaims);

            await userManager.AddClaimsAsync(user, claims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signinCredentials);

            return jwtSecurityToken;
        }

        private async Task<bool> ManageRole(string roleName)
        {
            var result = await roleManager.RoleExistsAsync("Admin");
            if(!result)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }

            result = await roleManager.RoleExistsAsync("AppUser");
            if(!result)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "AppUser" });
            }

            return await roleManager.RoleExistsAsync(roleName);
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
}
