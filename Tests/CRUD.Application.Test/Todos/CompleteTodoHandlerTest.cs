using CRUD.Application.Todos.CompleteTodo;
using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Exceptions;
using CRUD.Domain.Todos.Models;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Test.Todos
{
    public sealed class CompleteTodoHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldCloseTodo_WhenTodoExists()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var handler = new CompleteTodoHandler(todoRepository, unitOfWork);
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());
            todoRepository.GetById(todo.Id).Returns(todo);
            var command = new CompleteTodoCommand(todo.Id);


            await handler.Handle(command, CancellationToken.None);


            todoRepository.Received(1).Update(todo);
            unitOfWork.Received(1).SaveChanges();
        }

        [Fact]
        public async Task Handle_ShouldThrowTodoNotFoundException_WhenTodoDoesNotExist()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var handler = new CompleteTodoHandler(todoRepository, unitOfWork);
            var command = new CompleteTodoCommand(Guid.NewGuid());


            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);


            await act.Should().ThrowAsync<TodoNotFoundException>();
            unitOfWork.DidNotReceive().SaveChanges();
            await unitOfWork.DidNotReceive().SaveChangesAsync();
        }


        [Fact]
        public async Task Handle_ShouldCloseTodo_WhenTodoDontHaveClient()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var handler = new CompleteTodoHandler(todoRepository, unitOfWork);
            var todo = Todo.Create("Test Todo", "Test Description", null);
            todoRepository.GetById(todo.Id).Returns(todo);
            var command = new CompleteTodoCommand(todo.Id);


            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);


            await act.Should().ThrowAsync<NeedHaveClientTodoException>();
            unitOfWork.DidNotReceive().SaveChanges();
            await unitOfWork.DidNotReceive().SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_Should_ThrowOperationCanceled_When_CancellationTokenIsCancelled()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());
            todoRepository.GetById(Arg.Any<Guid>()).Returns(todo);

            var handler = new CompleteTodoHandler(todoRepository, unitOfWork);
            var command = new CompleteTodoCommand(Guid.NewGuid());
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();


            Func<Task> act = async () => await handler.Handle(command, cancellationTokenSource.Token);


            await act.Should().ThrowAsync<OperationCanceledException>();
            unitOfWork.DidNotReceive().SaveChanges();
            await unitOfWork.DidNotReceive().SaveChangesAsync();
        }
    }
}
