using MediatR;
using System;

namespace Complejo.Application.Commands.Field
{
    public class DeleteFieldCommand : IRequest<bool>
    {
        public Guid? IdField { get; set; }

    }
}
