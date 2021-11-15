using Complejo.Application.Dtos.UiControls;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Queries.Turn;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Turn
{
    public class GetAllAvailableFieldByDateTimeHandler : IRequestHandler<GetAllAvailableFieldByDateTimeQuery, IList<ComboBoxDto>>
    {
        private readonly IFieldRepository fieldRepository;
        private readonly ITurnRepository turnRepository;
        private readonly IMapping mapping;

        public GetAllAvailableFieldByDateTimeHandler(IFieldRepository fieldRepository, ITurnRepository turnRepository, IMapping mapping)
        {
            this.fieldRepository = fieldRepository;
            this.turnRepository = turnRepository;
            this.mapping = mapping;
        }

        public async Task<IList<ComboBoxDto>> Handle(GetAllAvailableFieldByDateTimeQuery request, CancellationToken cancellationToken)
        {
            IList<Domain.Entities.Field> fields = await fieldRepository.ListAllAsync();

            if (!request.Date.HasValue || string.IsNullOrEmpty(request.Time))
            {
                return mapping.Map<IList<ComboBoxDto>>(fields);
            }

            var time = DateTime.Now;

            if (!string.IsNullOrEmpty(request.Time))
            {
                TimeSpan ts = new TimeSpan(int.Parse(request.Time), 0, 0);
                time = time.Date + ts;
            }

            IList<Domain.Entities.Turn> turns = await turnRepository.GetAllByDateAndTime(request.Date.Value, time);

            foreach (var turn in turns)
            {
                if (fields.Contains(turn.Field))
                {
                    fields.Remove(turn.Field);
                }
            }

            return mapping.Map<IList<ComboBoxDto>>(fields);
        }
    }
}
