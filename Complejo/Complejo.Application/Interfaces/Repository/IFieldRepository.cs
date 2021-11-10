using Complejo.Application.Interfaces.Repository.Base;
using Complejo.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Complejo.Application.Interfaces.Repository
{
    public interface IFieldRepository : IAsyncRepositoryBase<Field>
    {
        Task<bool> ExistSameDescription(string description, Guid? id);
    }
}
