using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Commands.Turn
{
    public class DeleteTurnCommand : IRequest<bool>
    {
        [Required]
        public Guid? IdTurn { get; set; }
    }
}
