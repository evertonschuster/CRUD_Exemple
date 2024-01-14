namespace CRUD.Application.Todos.UpdateTodo
{
    public sealed class UpdateTodoCommand : IRequest<UpdateTodoResult>
    {
        public UpdateTodoCommand()
        {
        }

        public UpdateTodoCommand(Guid id, string title, string description, Guid? clientId)
        {
            Id = id;
            Title = title;
            Description = description;
            ClientId = clientId;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid? ClientId { get; set; }
    }
}
