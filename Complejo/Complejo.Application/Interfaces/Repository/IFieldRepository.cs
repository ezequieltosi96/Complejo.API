using Complejo.Application.Interfaces.Repository.Base;
using Complejo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Complejo.Application.Interfaces.Repository
{
    public interface IFieldRepository : IAsyncRepositoryBase<Field>
    {
        Task<bool> ExistSameDescription(string description, Guid? id);

        Task<IList<Field>> ListAllAvailableAsync();

        Task<IList<Field>> ListAllAvailableByTypeAsync(Guid idFieldType);
    }
}
