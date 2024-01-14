using CRUD.Domain.Clients.Exceptions;
using CRUD.Domain.Clients.Models;

namespace CRUD.Domain.Test.Clients
{
    public sealed class EmailTest
    {
        [Fact]
        public void Should_ThrowException_When_EmailIsNull()
        {
            Assert.Throws<EmailNullOrWhitespaceException>(() => new Email(null!));
        }

        [Fact]
        public void Should_ThrowException_When_EmailIsEmpty()
        {
            Assert.Throws<EmailNullOrWhitespaceException>(() => new Email(string.Empty));
        }

        [Theory]
        [InlineData("invalidEmail")]
        [InlineData("invalidEmail@")]
        [InlineData("invalidEmail@qqq")]
        public void Should_ThrowException_When_EmailIsNotInCorrectFormat(string email)
        {
            Assert.Throws<EmailIncorrectFormatException>(() => new Email(email));
        }

        [Theory]
        [InlineData("test@example.com")]
        [InlineData("everton@gmail.com")]
        [InlineData("test.matheus@example.com")]
        public void Should_CreateEmail_When_ValidEmailIsProvided(string emailText)
        {
            var email = new Email(emailText);

            Assert.Equal(emailText, email.Value);
        }
    }
}
