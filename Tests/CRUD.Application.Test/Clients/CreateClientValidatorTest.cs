using FluentValidation.TestHelper;
using CRUD.Application.Clients.CreateClient;

namespace CRUD.Application.Test.Clients
{
    public sealed class CreateClientValidatorTest
    {
        [Fact]
        public void Handle_Should_HaveError_When_NameIsNull()
        {
            var validator = new CreateClientValidator();
            var comamnd = new CreateClientCommand();


            var result = validator.TestValidate(comamnd);


            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_NotHaveError_When_NameIsNotNull()
        {
            var validator = new CreateClientValidator();
            var comamnd = new CreateClientCommand("Everton", "Everton@gmail.com", "Developer");


            var result = validator.TestValidate(comamnd);


            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_HaveError_When_EmailIsNull()
        {
            var validator = new CreateClientValidator();
            var comamnd = new CreateClientCommand();


            var result = validator.TestValidate(comamnd);


            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_NotHaveError_When_EmailIsNotNull()
        {
            var validator = new CreateClientValidator();
            var comamnd = new CreateClientCommand("Everton", "Everton@gmail.com", "Developer");


            var result = validator.TestValidate(comamnd);


            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_HaveError_When_ProfessionIsNull()
        {
            var validator = new CreateClientValidator();
            var comamnd = new CreateClientCommand();


            var result = validator.TestValidate(comamnd);


            result.ShouldHaveValidationErrorFor(x => x.Profession);
        }

        [Fact]
        public void Should_NotHaveError_When_ProfessionIsNotNull()
        {
            var validator = new CreateClientValidator();
            var comamnd = new CreateClientCommand("Everton", "Everton@gmail.com", "Developer");


            var result = validator.TestValidate(comamnd);


            result.ShouldNotHaveValidationErrorFor(x => x.Profession);
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenNameEmailAndProfessionAreUnderMaxLength()
        {
            var validator = new CreateClientValidator();
            var command = new CreateClientCommand(new string('a', 100), new string('a', 40) + "@test.com", new string('a', 100));


            var result = validator.Validate(command);


            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenNameEmailOrProfessionExceedsMaxLength()
        {
            var validator = new CreateClientValidator();
            var command = new CreateClientCommand(new string('a', 101), new string('a', 101) + "@test.com", new string('a', 101));


            var result = validator.Validate(command);


            result.IsValid.Should().BeFalse();
        }
    }
}
