using MediatR;
using System;

namespace Complejo.Application.Commands.Turn
{
    public class CancelTurnCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
