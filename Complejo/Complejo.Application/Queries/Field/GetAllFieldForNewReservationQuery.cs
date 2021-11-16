using Complejo.Application.Dtos.Field;
using MediatR;
using System;
using System.Collections.Generic;

namespace Complejo.Application.Queries.Field
{
    public class GetAllFieldForNewReservationQuery : IRequest<IList<FieldByIdDto>>
    {
        public Guid IdFieldType { get; set; }

        public DateTime Date { get; set; }

        public string Time { get; set; }
    }
}
