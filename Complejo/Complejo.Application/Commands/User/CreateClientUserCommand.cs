using Complejo.Application.Commands.User.Base;
using MediatR;
using System;

namespace Complejo.Application.Commands.User
{
    public class CreateClientUserCommand : AbstractCreateUserCommand, IRequest<string>
    {
        public Guid IdClient { get; set; }
    }
}
