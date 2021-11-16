using MediatR;
using System;

namespace Complejo.Application.Commands.Turn
{
    public class CreateReservationLoggedInClientCommand : IRequest<string>
    {
        public DateTime Date { get; set; }

        public string Time { get; set; }

        public Guid IdField { get; set; }

        public Guid IdClient { get; set; }
    }
}
