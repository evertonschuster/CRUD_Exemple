using CRUD.Domain.Todos.Models;

namespace CRUD.Application.Todos.GetByIdTodo
{
    public sealed class GetByIdTodoResult
    {
        public GetByIdTodoResult(Todo model)
        {
            Id = model.Id;
            Title = model.Title;
            Description = model.Description;
            ClientId = model.ClientId;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Todo title
        /// </summary>
        /// <example>Todo title</example>
        public string Title { get; }

        /// <summary>
        /// Todo description
        /// </summary>
        /// <example>Todo description</example>
        public string Description { get; }

        /// <summary>
        /// Todo client id
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
        public Guid? ClientId { get; }

        /// <summary>
        /// Todo is completed for this client
        /// </summary>
        public bool IsCompleted { get; }

        /// <summary>
        /// Todo is closed ou deleted
        /// </summary>
        public bool IsClosed { get; }
    }
}
