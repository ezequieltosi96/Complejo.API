using Complejo.Application.Dtos.Field;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Complejo.Application.Queries.Field
{
    public class GetFieldByIdQuery : IRequest<FieldByIdDto>
    {
        [Required]
        public Guid? IdField { get; set; }
    }
}
