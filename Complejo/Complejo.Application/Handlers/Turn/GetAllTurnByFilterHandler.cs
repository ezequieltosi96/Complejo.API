using Complejo.Application.Dtos.Turn;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Queries.Turn;
using Complejo.Application.Responses;
using Complejo.Application.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Turn
{
    public class GetAllTurnByFilterHandler : IRequestHandler<GetAllTurnByFilterQuery, PagedListResponse<IList<TurnByFilterDto>>>
    {
        private readonly ITurnRepository turnRepository;
        private readonly IMapping mapping;

        public GetAllTurnByFilterHandler(ITurnRepository turnRepository, IMapping mapping)
        {
            this.turnRepository = turnRepository;
            this.mapping = mapping;
        }

        public async Task<PagedListResponse<IList<TurnByFilterDto>>> Handle(GetAllTurnByFilterQuery request, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.Now;

            if(!string.IsNullOrEmpty(request.Time))
            {
                TimeSpan ts = new TimeSpan(int.Parse(request.Time), 0, 0);
                dateTime = dateTime.Date + ts;
            }

            Expression<Func<Domain.Entities.Turn, bool>> predicate = x => request.ClientSearchCriteria != null ? x.Client.FullNameSearch.Contains(request.ClientSearchCriteria.ToUpper()) ||
                                                                                                                 x.Client.Dni.Equals(request.ClientSearchCriteria) : true &&
                                                                          request.IdFieldType.HasValue ? x.IdField == request.IdFieldType : true &&
                                                                          request.Date.HasValue ? x.Date.Date == request.Date.Value.Date : true;
                                                                          //request.Time != null ? x.Time.ToString("HH:mm") == dateTime.ToString("HH:mm") : true;

            Func<IQueryable<Domain.Entities.Turn>, IOrderedQueryable<Domain.Entities.Turn>> order = x => x.OrderBy(t => t.Date).ThenBy(t => t.Time);

            PagedList<Domain.Entities.Turn> turns = await turnRepository.GetAllByFilter(predicate, order, x => x.Include(t => t.Client).Include(x => x.Field), false, false, request.Page, request.Size);

            IList<TurnByFilterDto> dtos = mapping.Map<IList<TurnByFilterDto>>(turns.Where(x => request.Time != null ? x.Time.ToString("HH:mm") == dateTime.ToString("HH:mm") : true));

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
