using Complejo.Application.Interfaces.Identity;
using Complejo.Application.Interfaces.Security;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Complejo.API.Infrastructure.Security
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string UserId { get => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimsIdentity.DefaultNameClaimType); }
        public string UserName { get => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name); }
        public string Email { get => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email); }
    }
}
