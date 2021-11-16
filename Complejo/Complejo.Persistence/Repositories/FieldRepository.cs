using Complejo.Application.Interfaces.Repository;
using Complejo.Domain.Entities;
using Complejo.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Complejo.Persistence.Repositories
{
    public class FieldRepository : AsyncRepositoryBase<Field>, IFieldRepository
    {
        public FieldRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> ExistSameDescription(string description, Guid? id)
        {
            bool result = false;

            if(id.HasValue)
            {
                result = dbContext.Fields.Any(field => field.DescriptionSearch == description.Trim().ToUpper() && field.Id != id.Value);
            }
            else
            {
                result = dbContext.Fields.Any(field => field.DescriptionSearch == description.Trim().ToUpper());
            }

            return Task.FromResult(result);
        }

        public async Task<IList<Field>> ListAllAvailableAsync()
        {
            return await dbContext.Fields.Include(x => x.FieldStatus).Where(x => !x.Removed && x.FieldStatus.IdFieldStatusGroup == FieldStatus.AVAILABLE).ToListAsync();
        }

        public async Task<IList<Field>> ListAllAvailableByTypeAsync(Guid idFieldType)
        {
            return await dbContext.Fields.Include(x => x.FieldStatus).Include(x => x.FieldType).Where(x => !x.Removed && x.IdFieldType == idFieldType && x.FieldStatus.IdFieldStatusGroup == FieldStatus.AVAILABLE).ToListAsync();
        }
    }
}
