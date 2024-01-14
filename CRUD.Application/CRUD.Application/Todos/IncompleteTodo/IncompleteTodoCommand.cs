namespace CRUD.Application.Todos.IncompleteTodo
{
    public sealed class IncompleteTodoCommand : IRequest
    {
        public IncompleteTodoCommand()
        {
        }

        public IncompleteTodoCommand(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
