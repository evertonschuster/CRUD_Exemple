using CRUD.Domain.Commoms;

namespace CRUD.Domain.Clients.Exceptions
{
    public sealed class NameNullOrWhitespaceException : BusinessExeption
    {
        public NameNullOrWhitespaceException(string property) : base("Name cannot be null or whitespace.", property)
        {
        }
    }
}
