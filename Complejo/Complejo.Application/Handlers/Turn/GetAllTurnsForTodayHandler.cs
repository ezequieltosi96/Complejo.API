using Complejo.Application.Dtos.Turn;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Queries.Turn;
using Complejo.Application.Responses;
using Complejo.Application.Utils;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Turn
{
    public class GetAllTurnsForTodayHandler : IRequestHandler<GetAllTurnsForTodayQuery, PagedListResponse<IList<TurnByFilterDto>>>
    {
        private readonly ITurnRepository turnRepository;
        private readonly IMapping mapping;

        public GetAllTurnsForTodayHandler(ITurnRepository turnRepository, IMapping mapping)
        {
            this.turnRepository = turnRepository;
            this.mapping = mapping;
        }

        public async Task<PagedListResponse<IList<TurnByFilterDto>>> Handle(GetAllTurnsForTodayQuery request, CancellationToken cancellationToken)
        {
            PagedList<Domain.Entities.Turn> turns = await turnRepository.GetAllForToday(request.Page, request.Size);

            IList<TurnByFilterDto> dtos = mapping.Map<IList<TurnByFilterDto>>(turns);

            Metadata metadata = new Metadata
            {
                TotalCount = turns.TotalCount,
                TotalPages = turns.TotalPages,
                CurrentPage = turns.CurrentPage,
                PageSize = turns.PageSize,
                HasNextPage = turns.HasNextPage,
                HasPreviousPage = turns.HasPreviousPage,
                NextPageNumber = turns.NextPageNumber,
                PreviousPageNumber = turns.PreviousPageNumber,
            };

            return new PagedListResponse<IList<TurnByFilterDto>>(dtos, metadata);
        }
    }
}
