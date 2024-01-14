using CRUD.Domain.Todos.Models;

namespace CRUD.Application.Todos.CreateTodo
{
    public sealed class CreateTodoCommand : IRequest<CreateTodoResult>
    {
        public CreateTodoCommand()
        {
            Title = string.Empty;
            Description = string.Empty;
            ClientId = Guid.Empty;
        }

        public CreateTodoCommand(string title, string description, Guid? clientId)
        {
            Title = title;
            Description = description;
            ClientId = clientId;
        }

        /// <summary>
        /// Todo title
        /// </summary>
        /// <example>Todo title</example>
        public string Title { get; set; }

        /// <summary>
        /// Todo description
        /// </summary>
        /// <example>Todo description</example>
        public string Description { get; set; }

        /// <summary>
        /// Todo client id
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid? ClientId { get; set; }

        public Todo ToModel()
        {
            return Todo.Create(Title, Description, ClientId);
        }
    }
}
