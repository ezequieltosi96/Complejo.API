using Complejo.Application.Commands.User;
using Complejo.Application.Interfaces.Identity;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.User
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserService userService;
        private readonly IValidator<ResetPasswordCommand> validator;

        public ResetPasswordHandler(IUserService userService, IValidator<ResetPasswordCommand> validator)
        {
            this.userService = userService;
            this.validator = validator;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            await userService.ResetPassword(request.IdUser);

            return true;
        }
    }
}
