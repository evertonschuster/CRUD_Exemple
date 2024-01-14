using CRUD.Domain.Commoms;

namespace CRUD.Domain.Clients.Exceptions
{
    public sealed class EmailNullOrWhitespaceException(string? property) : BusinessExeption("Email cannot be null or whitespace.", property)
    {
    }
}
