using System.Text.RegularExpressions;
using CRUD.Domain.Clients.Exceptions;

namespace CRUD.Domain.Clients.Models
{
    public sealed partial record Email
    {
        private string _value;

        protected Email()
        {
            _value = string.Empty;
        }

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new EmailNullOrWhitespaceException(nameof(email));
            }

            var regex = EmailRegexPattern();

            if (!regex.IsMatch(email))
            {
                throw new EmailIncorrectFormatException(nameof(email));
            }

            _value = email;
        }

        public string Value => _value;

        public override string ToString()
        {
            return _value;
        }

        [GeneratedRegex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$")]
        private static partial Regex EmailRegexPattern();

        public static implicit operator string(Email email)
        {
            return email.ToString();
        }

        public static implicit operator Email(string email)
        {
            return new Email(email);
        }
    }
}
