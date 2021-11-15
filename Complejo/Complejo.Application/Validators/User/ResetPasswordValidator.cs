using Complejo.Application.Commands.User;
using Complejo.Application.Interfaces.Identity;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Validators.User
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        private readonly IUserService userService;

        public ResetPasswordValidator(IUserService userService)
        {
            this.userService = userService;

            RuleFor(x => x.IdUser)
                .NotNull().WithMessage("El Id del usuario es obligatorio.")
                .MustAsync(ExistUser).WithMessage("Id invalido.");
        }

        private async Task<bool> ExistUser(string id, CancellationToken cancellationToken)
        {
            return (await userService.GetUserById(id)) != null;
        }
    }
}
