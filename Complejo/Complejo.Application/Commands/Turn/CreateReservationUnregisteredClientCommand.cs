using MediatR;
using System;

namespace Complejo.Application.Commands.Turn
{
    public class CreateReservationUnregisteredClientCommand : IRequest<string>
    {
        public DateTime Date { get; set; }

        public string Time { get; set; }

        public Guid IdField { get; set; }

        public string ClientName { get; set; }

        public string ClientLastName { get; set; }

        public string ClientDni { get; set; }

        public string ClientPhoneNumber { get; set; }

        public string ClientEmail { get; set; }
    }
}
