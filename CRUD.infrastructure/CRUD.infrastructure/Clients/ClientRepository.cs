using CRUD.Domain.Clients.Models;
using CRUD.Domain.Clients.Repositories;

namespace CRUD.infrastructure.Clients
{
    internal sealed class ClientRepository(CRUDContext context) : Repository<Client>(context), IClientRepository
    {
    }
}
