using MediatR;
using System;

namespace Complejo.Application.Commands.Turn.Base
{
    public abstract class AbstractCreateUpdateTurnCommand : IRequest<Guid>
    {
        public DateTime Date { get; set; }

        public string Time { get; set; }

        public Guid IdField { get; set; }
    }
}
