using CRUD.Application.Todos.CloseTodo;
using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Exceptions;
using CRUD.Domain.Todos.Models;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Test.Todos
{
    public sealed class CloseTodoHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCloseTodo_WhenTodoExists()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var handler = new CloseTodoHandler(todoRepository, unitOfWork);
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());
            todoRepository.GetById(todo.Id).Returns(todo);
            var command = new CloseTodoCommand(todo.Id);


            await handler.Handle(command, CancellationToken.None);


            todo.IsClosed.Should().BeTrue();
            todoRepository.Received(1).Update(todo);
            unitOfWork.Received(1).SaveChanges();
        }

        [Fact]
        public async Task Handle_ShouldThrowTodoNotFoundException_WhenTodoDoesNotExist()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var handler = new CloseTodoHandler(todoRepository, unitOfWork);
            var command = new CloseTodoCommand(Guid.NewGuid());


            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);


            await act.Should().ThrowAsync<TodoNotFoundException>();
            todoRepository.DidNotReceive().Update(Arg.Any<Todo>());
            unitOfWork.DidNotReceive().SaveChanges();
        }

        [Fact]
        public async Task Handle_ShouldThrowOperationCanceled_When_CancellationTokenIsCancelled()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            todoRepository.GetById(Arg.Any<Guid>()).Returns(Todo.Create("Test Todo", "Test Description", Guid.NewGuid()));

            var handler = new CloseTodoHandler(todoRepository, unitOfWork);
            var command = new CloseTodoCommand(Guid.NewGuid());
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();


            Func<Task> act = async () => await handler.Handle(command, cancellationTokenSource.Token);


            await act.Should().ThrowAsync<OperationCanceledException>();
            unitOfWork.DidNotReceive().SaveChanges();
            await unitOfWork.DidNotReceive().SaveChangesAsync();
        }
    }
}
