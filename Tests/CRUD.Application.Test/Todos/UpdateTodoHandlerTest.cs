using CRUD.Application.Todos.UpdateTodo;
using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Exceptions;
using CRUD.Domain.Todos.Models;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Test.Todos
{
    public sealed class UpdateTodoHandlerTest
    {

        [Fact]
        public async Task Handle_Should_UpdateTodo_When_ValidCommandIsProvided()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var handler = new UpdateTodoHandler(todoRepository, unitOfWork);
            var command = new UpdateTodoCommand(Guid.NewGuid(), "test", "Developer", null);
            var todo = new Todo(command.Id, "Everton", "Description", null, null, null);


            todoRepository.GetById(command.Id).Returns(todo);
            var result = await handler.Handle(command, CancellationToken.None);


            todoRepository.Received(1).GetById(command.Id);
            todoRepository.Received(1).Update(todo);
            unitOfWork.Received(1).SaveChanges();
            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.Title.Should().Be(todo.Title);
            result.Description.Should().Be(todo.Description);
            result.ClientId.Should().Be(todo.ClientId);
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_TodoNotFound()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var handler = new UpdateTodoHandler(todoRepository, unitOfWork);
            var command = new UpdateTodoCommand(Guid.NewGuid(), "test", "Developer", null);


            Func<Task> act = async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            };


            await act.Should().ThrowAsync<TodoNotFoundException>();
            todoRepository.Received(1).GetById(Arg.Any<Guid>());
            todoRepository.Received(0).Delete(Arg.Any<Todo>());
            unitOfWork.Received(0).SaveChanges();
        }

        [Fact]
        public async Task Handle_Should_ThrowOperationCanceled_When_CancellationTokenIsCancelled()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            todoRepository.GetById(Arg.Any<Guid>())
                .Returns(new Todo(Guid.NewGuid(), "Everton", "Description", null, null, null));

            var handler = new UpdateTodoHandler(todoRepository, unitOfWork);
            var command = new UpdateTodoCommand(Guid.NewGuid(), "test", "Developer", null);
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();


            Func<Task> act = async () => await handler.Handle(command, cancellationTokenSource.Token);


            await act.Should().ThrowAsync<OperationCanceledException>();
            unitOfWork.DidNotReceive().SaveChanges();
            await unitOfWork.DidNotReceive().SaveChangesAsync();
        }
    }
}
