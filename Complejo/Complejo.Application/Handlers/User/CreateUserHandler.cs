using Complejo.Application.Commands.User;
using Complejo.Application.Interfaces.Identity;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserService userService;
        private readonly IValidator<CreateUserCommand> validator;

        public CreateUserHandler(IUserService userService, IValidator<CreateUserCommand> validator)
        {
            this.userService = userService;
            this.validator = validator;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            return await userService.CreateUser(request.Email, request.FirstName, request.LastName, request.RoleName);
        }
    }
}
