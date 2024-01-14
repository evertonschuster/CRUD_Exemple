using CRUD.Application.Todos.ListAllTodo;
using CRUD.Domain.Todos.Models;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Test.Todos
{
    public sealed class ListAllTodoHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnAllTodos()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var handler = new ListAllTodoHandler(todoRepository);

            var todos = new List<Todo>
            {
                new Todo(Guid.NewGuid(), "Title 1", "Description 1", null, null, null),
                new Todo(Guid.NewGuid(), "Title 2", "Description 2", null, null, null),
            };
            todoRepository.GetAll().Returns(todos);


            var result = await handler.Handle(new ListAllTodoQuery(), CancellationToken.None);


            result.Count.Should().Be(2);
            result.First().Title.Should().Be("Title 1");
            result.First().Description.Should().Be("Description 1");
            result.Last().Title.Should().Be("Title 2");
            result.Last().Description.Should().Be("Description 2");
        }
    }
}
