using Complejo.API.Controllers.Base;
using Complejo.Application.Interfaces.Identity;
using Complejo.Application.Models.Identity.Authentication;
using Complejo.Application.Models.Identity.Registration;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Complejo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            return Ok(await authenticationService.RegisterAsync(request));
        }

    }
}
