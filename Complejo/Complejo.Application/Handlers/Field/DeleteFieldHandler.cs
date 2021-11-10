using Complejo.Application.Commands.Field;
using Complejo.Application.Exceptions;
using Complejo.Application.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Application.Handlers.Field
{
    public class DeleteFieldHandler : IRequestHandler<DeleteFieldCommand, bool>
    {
        private readonly IFieldRepository fieldRepository;

        public DeleteFieldHandler(IFieldRepository fieldRepository)
        {
            this.fieldRepository = fieldRepository;
        }

        public async Task<bool> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Field field = await fieldRepository.GetByIdAsync(request.IdField.Value);

            if(field == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Field), request.IdField);
            }

            await fieldRepository.DeleteAsync(field);

            return true;
        }
    }
}
