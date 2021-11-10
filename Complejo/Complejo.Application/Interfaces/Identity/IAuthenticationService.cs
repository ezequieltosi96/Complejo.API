using Complejo.Application.Models.Identity.Authentication;
using Complejo.Application.Models.Identity.Registration;
using System.Threading.Tasks;

namespace Complejo.Application.Interfaces.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);

        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
