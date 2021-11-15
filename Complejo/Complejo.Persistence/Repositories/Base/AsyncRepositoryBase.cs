using Complejo.Application.Interfaces.Repository.Base;
using Complejo.Application.Responses;
using Complejo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Complejo.Persistence.Repositories.Base
{
    public class AsyncRepositoryBase<T> : IAsyncRepositoryBase<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext dbContext;

        public AsyncRepositoryBase(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            entity.Removed = !entity.Removed;

            await UpdateAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IList<T>> ListAllAsync(bool includeRemoved = false)
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (!includeRemoved)
            {
                return await query.Where(x => x.Removed == false).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size)
        {
            return await dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public Task<bool> Exist(Guid id)
        {
            bool exist = dbContext.Set<T>().Any(x => x.Id == id && !x.Removed);

            return Task.FromResult(exist);
        }

        public async Task<PagedList<T>> GetAllByFilter(Expression<Func<T, bool>> predicate = null, 
                                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                                       Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, 
                                                       bool includeRemoved = false, 
                                                       bool enabledTraking = true, 
                                                       int page = 1, 
                                                       int size = 10)
        {
            IQueryable<T> query = dbContext.Set<T>();

            int count = query.Count();

            if (enabledTraking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            if (!includeRemoved)
            {
                query = query.Where(x => x.Removed == false);
            }

            IList<T> entities = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new PagedList<T>(entities, count, page, size);
        }
    }
}
