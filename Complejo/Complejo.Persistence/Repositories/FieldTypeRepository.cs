using Complejo.Application.Interfaces.Repository;
using Complejo.Domain.Entities;
using Complejo.Persistence.Repositories.Base;

namespace Complejo.Persistence.Repositories
{
    public class FieldTypeRepository : AsyncRepositoryBase<FieldType>, IFieldTypeRepository
    {
        public FieldTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
