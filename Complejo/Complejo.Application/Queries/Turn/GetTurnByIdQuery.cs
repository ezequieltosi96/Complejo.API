using Complejo.Application.Dtos.Turn;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Queries.Turn
{
    public class GetTurnByIdQuery : IRequest<TurnByIdDto>
    {
        [Required]
        public Guid? IdTurn { get; set; }
    }
}
