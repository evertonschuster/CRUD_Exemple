using FluentValidation.TestHelper;
using CRUD.Application.Clients.UpdateClient;

namespace CRUD.Application.Test.Clients
{
    public sealed class UpdateClientValidatorTest
    {

        [Fact]
        public void Handle_Should_HaveError_When_EmailIsNull()
        {
            var validator = new UpdateClientValidator();
            var comamnd = new UpdateClientCommand();


            var result = validator.TestValidate(comamnd);


            result.ShouldHaveValidationErrorFor(x => x.Email);
        }


        [Fact]
        public void Handle_Should_NotHaveError_When_EmailIsNotNull()
        {
            var validator = new UpdateClientValidator();
            var comamnd = new UpdateClientCommand(Guid.NewGuid(), "eveton.schuster@gmail.com", "Tester");


            var result = validator.TestValidate(comamnd);


            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Handle_Should_HaveError_When_ProfessionIsNull()
        {
            var validator = new UpdateClientValidator();
            var comamnd = new UpdateClientCommand();


            var result = validator.TestValidate(comamnd);


            result.ShouldHaveValidationErrorFor(x => x.Profession);
        }

        [Fact]
        public void Handle_Should_NotHaveError_When_ProfessionIsNotNull()
        {
            var validator = new UpdateClientValidator();
            var comamnd = new UpdateClientCommand(Guid.NewGuid(), "eveton.schuster@gmail.com", "Tester");


            var result = validator.TestValidate(comamnd);


            result.ShouldNotHaveValidationErrorFor(x => x.Profession);
        }
    }
}
