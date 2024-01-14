using CRUD.Domain.Todos.Events;
using CRUD.Domain.Todos.Exceptions;
using CRUD.Domain.Todos.Models;

namespace CRUD.Domain.Test.Todos
{
    public sealed class TodoTest
    {
        [Fact]
        public void Todo_ShouldSet_Properties()
        {
            var id = Guid.NewGuid();
            var title = "Test Todo";
            var description = "Test Description";
            var clientId = Guid.NewGuid();


            var todo = new Todo(id, title, description, null, null, clientId);


            Assert.Equal(id, todo.Id);
            Assert.Equal(title, todo.Title);
            Assert.Equal(description, todo.Description);
            Assert.Equal(clientId, todo.ClientId);
            Assert.Null(todo.IsCompletedAt);
        }

        [Fact]
        public void Complete_ShouldSet_IsCompletedAt()
        {
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());


            todo.Complete();


            Assert.NotNull(todo.IsCompletedAt);
            Assert.Collection(todo.Events,
                @event =>
                {
                    Assert.IsType<CreatedTodoEvent>(@event);
                    Assert.Equal(todo.Title, ((CreatedTodoEvent)@event).Title);
                    Assert.Equal(todo.Description, ((CreatedTodoEvent)@event).Description);
                    Assert.Equal(todo.ClientId, ((CreatedTodoEvent)@event).ClientId);
                },
                @event =>
                {
                    Assert.IsType<CompletedTodoEvent>(@event);
                }
            );
        }

        [Fact]
        public void Delete_ShouldSet_Deleted()
        {
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());


            todo.Delete();


            Assert.NotNull(todo.DeletedAt);
            Assert.Collection(todo.Events,
                @event =>
                {
                    Assert.IsType<CreatedTodoEvent>(@event);
                },
                @event =>
                {
                    Assert.IsType<DeletedTodoEvent>(@event);
                }
            );
        }

        [Fact]
        public void Incomplete_ShouldSet_IsCompletedAtToNull()
        {
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());


            todo.Incomplete();


            Assert.Null(todo.IsCompletedAt);
            Assert.Collection(todo.Events,
                @event =>
                {
                    Assert.IsType<CreatedTodoEvent>(@event);
                },
                @event =>
                {
                    Assert.IsType<IncompletedTodoEvent>(@event);
                }
            );
        }

        [Fact]
        public void Update_ShouldUpdate_Properties()
        {
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());
            var newTitle = "New Title";
            var newDescription = "New Description";
            var newClientId = Guid.NewGuid();


            todo.Update(newTitle, newDescription, newClientId);


            Assert.Equal(newTitle, todo.Title);
            Assert.Equal(newDescription, todo.Description);
            Assert.Equal(newClientId, todo.ClientId);
            Assert.Collection(todo.Events,
               @event =>
               {
                   Assert.IsType<CreatedTodoEvent>(@event);
               },
               @event =>
               {
                   Assert.IsType<UpdatedTodoEvent>(@event);
                   Assert.Equal(todo.Title, ((UpdatedTodoEvent)@event).Title);
                   Assert.Equal(todo.Description, ((UpdatedTodoEvent)@event).Description);
                   Assert.Equal(todo.ClientId, ((UpdatedTodoEvent)@event).ClientId);
               }
           );
        }

        [Fact]
        public void Close_ShouldSet_IsClosedAt()
        {
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());


            todo.Close();


            Assert.NotNull(todo.IsClosedAt);
        }

        [Fact]
        public void Complete_ShouldThrow_NeedHaveClientTodoException_When_ClientId_IsNull()
        {
            var todo = new Todo("Test Todo", "Test Description", null, null, null);

            Assert.Throws<NeedHaveClientTodoException>(() => todo.Complete());
        }

        [Fact]
        public void IsCompleted_ShouldReturn_True_When_IsCompletedAt_IsNotNull()
        {
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());


            todo.Complete();


            Assert.True(todo.IsCompleted);
        }

        [Fact]
        public void IsCompleted_ShouldReturn_False_When_IsCompletedAt_IsNull()
        {
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());


            todo.Incomplete();


            Assert.False(todo.IsCompleted);
        }

        [Fact]
        public void IsClosed_ShouldReturn_True_When_IsClosedAt_IsNotNull()
        {
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());


            todo.Close();


            Assert.True(todo.IsClosed);
        }

        [Fact]
        public void IsClosed_ShouldReturn_False_When_IsClosedAt_IsNull()
        {
            var todo = Todo.Create("Test Todo", "Test Description", Guid.NewGuid());


            Assert.False(todo.IsClosed);
        }
    }
}