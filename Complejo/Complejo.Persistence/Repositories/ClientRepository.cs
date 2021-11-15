using Complejo.Application.Interfaces.Repository;
using Complejo.Domain.Entities;
using Complejo.Persistence.Repositories.Base;
using System.Linq;

namespace Complejo.Persistence.Repositories
{
    public class ClientRepository : AsyncRepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Client GetClientByDni(string dni)
        {
            return dbContext.Clients.Where(x => x.Dni == dni && !x.Removed).FirstOrDefault();
        }
    }
}
