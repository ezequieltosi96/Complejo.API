using MediatR;
using System;

namespace Complejo.Application.Commands.Field.Base
{
    public class AbstractCreateUpdateFieldCommand : IRequest<Guid>
    {
        public string Description { get; set; }

        public Guid? IdFieldType { get; set; }

        public Guid? IdFieldStatus { get; set; }

        public decimal Price { get; set; }
    }
}
