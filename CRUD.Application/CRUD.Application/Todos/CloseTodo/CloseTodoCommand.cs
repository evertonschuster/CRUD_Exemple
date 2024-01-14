namespace CRUD.Application.Todos.CloseTodo
{
    public sealed class CloseTodoCommand : IRequest
    {
        public CloseTodoCommand()
        {
        }

        public CloseTodoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
