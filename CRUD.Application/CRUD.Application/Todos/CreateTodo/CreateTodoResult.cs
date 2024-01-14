namespace CRUD.Application.Todos.CreateTodo
{
    public sealed class CreateTodoResult
    {
        public CreateTodoResult(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
