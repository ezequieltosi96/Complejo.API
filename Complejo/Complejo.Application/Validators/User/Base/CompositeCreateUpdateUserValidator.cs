﻿using Complejo.Application.Commands.User.Base;
using FluentValidation;

namespace Complejo.Application.Validators.User.Base
{
    public class CompositeCreateUpdateUserValidator : AbstractValidator<AbstractCreateUpdateUserCommand>
    {

        public CompositeCreateUpdateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El correo es obligatorio.")
                .EmailAddress()
                    .WithMessage("Debe ingresar un email valido.");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El nombre es obligatorio.");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                    .WithMessage("El apellido es obligatorio.");
        }
    }
}
