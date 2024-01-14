using CRUD.Domain.Commoms;

namespace CRUD.Domain.Clients.Exceptions
{
    public sealed class EmailIncorrectFormatException(string? property) : BusinessExeption("Email is not in a correct format.", property)
    {
    }
}
