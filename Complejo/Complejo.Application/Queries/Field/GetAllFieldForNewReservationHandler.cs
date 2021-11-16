using Complejo.Application.Dtos.Field;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Queries.Field
{
    public class GetAllFieldForNewReservationHandler : IRequestHandler<GetAllFieldForNewReservationQuery, IList<FieldByIdDto>>
    {
        private readonly IFieldRepository fieldRepository;
        private readonly ITurnRepository turnRepository;
        private readonly IMapping mapping;

        public GetAllFieldForNewReservationHandler(IFieldRepository fieldRepository, ITurnRepository turnRepository, IMapping mapping)
        {
            this.fieldRepository = fieldRepository;
            this.turnRepository = turnRepository;
            this.mapping = mapping;
        }

        public async Task<IList<FieldByIdDto>> Handle(GetAllFieldForNewReservationQuery request, CancellationToken cancellationToken)
        {
            IList<Domain.Entities.Field> fields = await fieldRepository.ListAllAvailableByTypeAsync(request.IdFieldType);

            var time = DateTime.Now;

            if (!string.IsNullOrEmpty(request.Time))
            {
                TimeSpan ts = new TimeSpan(int.Parse(request.Time), 0, 0);
                time = time.Date + ts;
            }

            IList<Domain.Entities.Turn> turns = await turnRepository.GetAllByDateAndTime(request.Date, time);

            foreach (var turn in turns)
            {
                if (fields.Contains(turn.Field))
                {
                    fields.Remove(turn.Field);
                }
            }

            return mapping.Map<IList<FieldByIdDto>>(fields);
        }
    }
}
