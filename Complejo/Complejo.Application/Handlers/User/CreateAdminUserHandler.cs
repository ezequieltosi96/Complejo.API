using Complejo.Application.Commands.User;
using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Identity;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.User
{
    public class CreateAdminUserHandler : IRequestHandler<CreateAdminUserCommand, string>
    {
        private readonly IUserService userService;
        private readonly IValidator<CreateAdminUserCommand> validator;

        public CreateAdminUserHandler(IUserService userService, IValidator<CreateAdminUserCommand> validator)
        {
            this.userService = userService;
            this.validator = validator;
        }

        public async Task<string> Handle(CreateAdminUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            string result = await userService.CreateAdminUser(request.Email, request.FirstName, request.LastName);

            if(result == null)
            {
                throw new BadRequestException("Ocurrió un error al crear el usuario.");
            }

            return result;
        }
    }
}
