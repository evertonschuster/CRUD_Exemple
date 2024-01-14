using CRUD.Application.Todos.CreateTodo;
using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Models;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Test.Todos
{
    public sealed class CreateTodoHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateTodo_WhenValidRequest()
        {

            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var handler = new CreateTodoHandler(todoRepository, unitOfWork);

            var command = new CreateTodoCommand("Test Todo", "Test Description", Guid.NewGuid());


            var result = await handler.Handle(command, CancellationToken.None);


            result.Should().NotBeNull();
            todoRepository.Received(1).Add(Arg.Any<Todo>());
            unitOfWork.Received(1).SaveChanges();
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenCancellationRequested()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var handler = new CreateTodoHandler(todoRepository, unitOfWork);

            var command = new CreateTodoCommand("Test Todo", "Test Description", Guid.NewGuid());
            var cancellationToken = new CancellationToken(true);


            Func<Task> act = async () => await handler.Handle(command, cancellationToken);



            await act.Should().ThrowAsync<OperationCanceledException>();
        }

        [Fact]
        public void ToModel_Should_Return_Client_With_Same_Properties_As_Command()
        {
            var command = new CreateTodoCommand("Test Todo", "Test Description", Guid.NewGuid());


            var model = command.ToModel();


            model.Should().BeOfType<Todo>();
            model.Title.ToString().Should().Be(command.Title);
            model.Description.ToString().Should().Be(command.Description);
            model.ClientId.Should().Be(command.ClientId);
        }
    }
}
