using CRUD.Domain.Todos.Models;

namespace CRUD.Application.Todos.ListAllTodo
{
    public sealed class ListAllTodoResult
    {
        public ListAllTodoResult(Todo model)
        {
            Id = model.Id;
            Title = model.Title;
            Description = model.Description;
            ClientId = model.ClientId;
            IsCompleted = model.IsCompleted;
            IsClosed = model.IsClosed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000000</example>
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
        /// Is true when todo completed
        /// </summary>
        public bool IsCompleted { get; }

        /// <summary>
        /// Is false when todo closed
        /// </summary>
        public bool IsClosed { get; }
    }
}
