using MediatR;
using System;

namespace Complejo.Application.Commands.Client
{
    public class CreateClientCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Dni { get; set; }
    }
}
