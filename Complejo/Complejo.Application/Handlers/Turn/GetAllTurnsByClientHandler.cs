using Complejo.Application.Dtos.Turn;
using Complejo.Application.Interfaces.Mapping;
using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Queries.Turn;
using Complejo.Application.Responses;
using Complejo.Application.Utils;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Turn
{
    public class GetAllTurnsByClientHandler : IRequestHandler<GetAllTurnsByClientQuery, PagedListResponse<IList<TurnByClientDto>>>
    {
        private readonly IValidator<GetAllTurnsByClientQuery> validator;

        private readonly ITurnRepository turnRepository;
        private readonly IMapping mapping;

        public GetAllTurnsByClientHandler(ITurnRepository turnRepository, IMapping mapping, IValidator<GetAllTurnsByClientQuery> validator)
        {
            this.turnRepository = turnRepository;
            this.mapping = mapping;
            this.validator = validator;
        }

        public async Task<PagedListResponse<IList<TurnByClientDto>>> Handle(GetAllTurnsByClientQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            PagedList<Domain.Entities.Turn> turns = await turnRepository.GetAllByClient(request.IdClient.Value, request.Page, request.Size);

            IList<TurnByClientDto> dtos = mapping.Map<IList<TurnByClientDto>>(turns);

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

            return new PagedListResponse<IList<TurnByClientDto>>(dtos, metadata);
        }
    }
}
