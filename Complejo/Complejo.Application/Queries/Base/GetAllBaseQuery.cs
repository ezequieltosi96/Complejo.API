using Complejo.Application.Dtos.UiControls;
using MediatR;
using System;
using System.Collections.Generic;

namespace Complejo.Application.Queries.Base
{
    public class GetAllBaseQuery : IRequest<IEnumerable<ComboBoxDto>>
    {
        public Guid? IdEntity { get; set; }
    }
}
