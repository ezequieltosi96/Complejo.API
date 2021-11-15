using Complejo.Application.Interfaces.Repository.Base;
using Complejo.Application.Responses;
using Complejo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Complejo.Application.Interfaces.Repository
{
    public interface ITurnRepository : IAsyncRepositoryBase<Turn>
    {
        bool IsTurnAvailable(DateTime time, Guid idField);

        Task<PagedList<Turn>> GetAllForToday(int page, int size);

        Task<PagedList<Turn>> GetAllByClient(Guid idClient, int page, int size);
        
        Task<IList<Turn>> GetAllByDateAndTime(DateTime date, DateTime time);
    }
}
