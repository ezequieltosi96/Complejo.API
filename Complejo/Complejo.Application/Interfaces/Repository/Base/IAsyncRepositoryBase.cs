using Complejo.Application.Responses;
using Complejo.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Complejo.Application.Interfaces.Repository.Base
{
    public interface IAsyncRepositoryBase<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(Guid id);

        Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<IList<T>> ListAllAsync(bool includeRemoved = false);

        Task<PagedList<T>> GetAllByFilter(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool includeRemoved = false, bool enabledTraking = true, int page = 1, int size = 10);

        Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size);

        Task<bool> Exist(Guid id);
    }
}
