namespace CRUD.Application.Todos.CompleteTodo
{
    public sealed class CompleteTodoCommand : IRequest
    {
        public CompleteTodoCommand()
        {
        }

        public CompleteTodoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
