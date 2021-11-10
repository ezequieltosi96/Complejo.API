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
            var existingUser = await userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.UserName}' already exists.");
            }

            var roleResult = await ManageRole(request.RoleName);
            if(!roleResult)
            {
                throw new BadRequestException("An error has occured while register the user.");
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
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
                new Claim("isAdmin", roles.Contains("Admin").ToString())
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
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
    }
}
