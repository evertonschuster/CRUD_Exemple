using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CRUD.Domain.Clients.Models;

namespace CRUD.infrastructure.Converters
{
    internal sealed class NameConverter : ValueConverter<Name, string>
    {
        public NameConverter()
            : base(v => v.Value, v => new Name(v))
        {
        }
    }
}
