using Complejo.Application.Commands.User;
using Complejo.Application.Interfaces.Identity;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.User
{
    public class CreateClientUserHandler : IRequestHandler<CreateClientUserCommand, string>
    {
        private readonly IUserService userService;
        private readonly IValidator<CreateClientUserCommand> validator;

        public CreateClientUserHandler(IUserService userService, IValidator<CreateClientUserCommand> validator)
        {
            this.userService = userService;
            this.validator = validator;
        }

        public async Task<string> Handle(CreateClientUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            return await userService.CreateAppUserUser(request.Email, request.FirstName, request.LastName, request.IdClient);
        }
    }
}
