using CRUD.Domain.Clients.Exceptions;

namespace CRUD.Domain.Clients.Models
{
    public sealed record Name
    {
        private string _value;

        protected Name()
        {
            _value = string.Empty;
        }

        public Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new NameNullOrWhitespaceException(nameof(name));
            }

            _value = name;
        }

        public string Value => _value;

        public override string ToString()
        {
            return _value;
        }

        public static implicit operator string(Name nome)
        {
            return nome.ToString();
        }

        public static implicit operator Name(string nome)
        {
            return new Name(nome);
        }
    }
}
