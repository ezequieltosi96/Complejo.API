using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Responses;
using Complejo.Domain.Entities;
using Complejo.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Complejo.Persistence.Repositories
{
    public class TurnRepository : AsyncRepositoryBase<Turn>, ITurnRepository
    {
        public TurnRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedList<Turn>> GetAllByClient(Guid idClient, int page, int size)
        {
            IQueryable<Turn> query = dbContext.Turns;

            int count = query.Where(t => t.IdClient == idClient && !t.Removed).Count();

            IList<Turn> turns = await query.Include(x => x.Field).Include(x => x.Field.FieldType).Where(t => t.IdClient == idClient && !t.Removed)
                                           .Skip((page - 1) * size).Take(size).OrderByDescending(t => t.Time).ToListAsync();

            return new PagedList<Turn>(turns, count, page, size);
        }

        public async Task<IList<Turn>> GetAllByDateAndTime(DateTime date, DateTime time)
        {
            IList<Turn> turns = await dbContext.Turns.Include(x => x.Field).Where(x => !x.Removed && x.Date.Date == date.Date).ToListAsync();

            return turns.Where(x => x.Time.ToString("HH:mm") == time.ToString("HH:mm")).ToList();
        }

        public async Task<PagedList<Turn>> GetAllForToday(int page, int size)
        {
            IQueryable<Turn> query = dbContext.Turns;

            int count = query.Count();

            IList<Turn> turns = await query.Where(t => t.Date.Date == DateTime.Today.Date && !t.Removed)
                                           .Skip((page - 1) * size).Take(size).ToListAsync();

            return new PagedList<Turn>(turns, count, page, size);
        }

        public Task<Turn> GetByCode(string code)
        {
            return dbContext.Turns.Include(x => x.Field).Include(x => x.Field.FieldType).Include(x => x.Client).Where(x => !x.Removed && x.Code == code).FirstOrDefaultAsync();
        }

        public bool IsTurnAvailable(DateTime time, Guid idField)
        {
            return dbContext.Turns.Where(x => x.Time == time && x.IdField == idField && !x.Removed).Count() > 0;
        }
    }
}
