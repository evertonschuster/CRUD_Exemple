using CRUD.Application.Todos.GetByIdTodo;
using CRUD.Domain.Todos.Exceptions;
using CRUD.Domain.Todos.Models;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Test.Todos
{
    public sealed class GetByIdTodoHandlerTest
    {
        [Fact]
        public async Task Handle_Should_Return_GetByIdTodoResult_When_Todo_Exists()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var todo = new Todo(Guid.NewGuid(), "Test", "description", null, null, null);
            todoRepository.GetById(todo.Id).Returns(todo);

            var query = new GetByIdTodoQuery(todo.Id);
            var handler = new GetByIdTodoHandler(todoRepository);


            var result = await handler.Handle(query, CancellationToken.None);


            result.Should().NotBeNull();
            result.Title.Should().Be(todo.Title);
            result.Description.Should().Be(todo.Description);
            result.ClientId.Should().Be(todo.ClientId);
        }

        [Fact]
        public async Task Handle_Should_Throw_TodoNotFoundException_When_Todo_Does_Not_Exist()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var handler = new GetByIdTodoHandler(todoRepository);
            var command = new GetByIdTodoQuery(Guid.NewGuid());


            var act = async () => await handler.Handle(command, CancellationToken.None);


            await Assert.ThrowsAsync<TodoNotFoundException>(act);
        }
    }
}
