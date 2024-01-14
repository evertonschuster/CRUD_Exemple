using CRUD.Application.Todos.UpdateTodo;
using CRUD.Domain.Clients.Models;
using CRUD.Domain.Clients.Repositories;

namespace CRUD.Application.Test.Todos
{
    public sealed class UpdateTodoValidatorTest
    {
        [Fact]
        public void Validate_ShouldReturnTrue_WhenTitleAndDescriptionAreNotEmptyAndClientExists()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var validator = new UpdateTodoValidator(clientRepository);

            var client = new Client(Guid.NewGuid(), "Everton", "Everton.schuster@gmail.com", "Developer");
            clientRepository.GetById(client.Id).Returns(client);
            var command = new UpdateTodoCommand(Guid.NewGuid(), "Test Title", "Test Description", client.Id);


            var result = validator.Validate(command);


            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenTitleOrDescriptionIsEmptyOrClientDoesNotExist()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var validator = new UpdateTodoValidator(clientRepository);

            var command = new UpdateTodoCommand(Guid.NewGuid(), "Title", "Test Description", Guid.NewGuid());


            var result = validator.Validate(command);


            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenTitleAndDescriptionAreUnderMaxLength()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var validator = new UpdateTodoValidator(clientRepository);

            var client = new Client(Guid.NewGuid(), "Everton", "Everton.schuster@gmail.com", "Developer");
            clientRepository.GetById(client.Id).Returns(client);

            var command = new UpdateTodoCommand(Guid.NewGuid(), new string('a', 100), new string('a', 100), client.Id);


            var result = validator.Validate(command);


            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenTitleOrDescriptionExceedsMaxLength()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var validator = new UpdateTodoValidator(clientRepository);

            var client = new Client(Guid.NewGuid(), "Everton", "Everton.schuster@gmail.com", "Developer");
            clientRepository.GetById(client.Id).Returns(client);

            var command = new UpdateTodoCommand(Guid.NewGuid(), new string('a', 101), new string('a', 101), client.Id);


            var result = validator.Validate(command);


            result.IsValid.Should().BeFalse();
        }
    }
}
