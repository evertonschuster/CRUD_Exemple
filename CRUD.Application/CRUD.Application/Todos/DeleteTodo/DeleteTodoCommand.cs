namespace CRUD.Application.Todos.DeleteTodo
{
    public sealed class DeleteTodoCommand : IRequest
    {
        public DeleteTodoCommand()
        {
        }

        public DeleteTodoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
