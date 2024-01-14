using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CRUD.Domain.Clients.Models;

namespace CRUD.infrastructure.Converters
{
    internal sealed class EmailConverter : ValueConverter<Email, string>
    {
        public EmailConverter()
            : base(v => v.Value, v => new Email(v))
        {
        }
    }
}
