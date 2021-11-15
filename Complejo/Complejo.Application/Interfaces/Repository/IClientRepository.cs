using Complejo.Application.Interfaces.Repository.Base;
using Complejo.Domain.Entities;
using System.Threading.Tasks;

namespace Complejo.Application.Interfaces.Repository
{
    public interface IClientRepository : IAsyncRepositoryBase<Client>
    {
        Client GetClientByDni(string dni);
    }
}
