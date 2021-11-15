using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Commands.Field
{
    public class DeleteFieldCommand : IRequest<bool>
    {
        [Required]
        public Guid? IdField { get; set; }

    }
}
