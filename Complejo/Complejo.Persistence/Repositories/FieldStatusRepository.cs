using Complejo.Application.Interfaces.Repository;
using Complejo.Domain.Entities;
using Complejo.Persistence.Repositories.Base;

namespace Complejo.Persistence.Repositories
{
    public class FieldStatusRepository : AsyncRepositoryBase<FieldStatus>, IFieldStatusRepository
    {
        public FieldStatusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
