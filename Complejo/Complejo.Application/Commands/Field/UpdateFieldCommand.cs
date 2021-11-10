using Complejo.Application.Commands.Field.Base;
using System;

namespace Complejo.Application.Commands.Field
{
    public class UpdateFieldCommand : AbstractCreateUpdateFieldCommand
    {
        public Guid? IdField { get; set; }
    }
}
