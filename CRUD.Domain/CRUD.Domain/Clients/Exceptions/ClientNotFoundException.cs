using CRUD.Domain.Commoms;

namespace CRUD.Domain.Clients.Exceptions
{
    public sealed class ClientNotFoundException : BusinessExeption
    {
        public ClientNotFoundException() : base("Client not found.", string.Empty)
        {
        }
    }
}
