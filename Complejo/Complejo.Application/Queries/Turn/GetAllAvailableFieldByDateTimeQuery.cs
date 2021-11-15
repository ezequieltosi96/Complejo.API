using Complejo.Application.Dtos.UiControls;
using MediatR;
using System;
using System.Collections.Generic;

namespace Complejo.Application.Queries.Turn
{
    public class GetAllAvailableFieldByDateTimeQuery : IRequest<IList<ComboBoxDto>>
    {
        public DateTime? Date { get; set; }

        public string Time { get; set; }
    }
}
