using Complejo.Application.Commands.User;
using Complejo.Application.Interfaces.Identity;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly IUserService userService;
        private readonly IValidator<UpdateUserCommand> validator;

        public UpdateUserHandler(IUserService userService, IValidator<UpdateUserCommand> validator)
        {
            this.userService = userService;
            this.validator = validator;
        }

        public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            return await userService.UpdateUser(request.Id, request.Email, request.FirstName, request.LastName);
        }
    }
}
