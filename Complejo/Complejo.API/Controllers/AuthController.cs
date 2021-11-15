using Complejo.API.Controllers.Base;
using Complejo.Application.Commands.Client;
using Complejo.Application.Interfaces.Identity;
using Complejo.Application.Models.Identity.Authentication;
using Complejo.Application.Models.Identity.Registration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Complejo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IMediator mediator;

        public AuthController(IAuthenticationService authenticationService, IMediator mediator)
        {
            this.authenticationService = authenticationService;
            this.mediator = mediator;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            var result = await mediator.Send(new CreateClientCommand 
                                            { 
                                                FirstName = request.FirstName,
                                                LastName = request.LastName,
                                                PhoneNumber = request.PhoneNumber,
                                                Dni = request.Dni,
                                            });
            
            request.IdClient = result;

            return Ok(await authenticationService.RegisterAsync(request));
        }

    }
}
