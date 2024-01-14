using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Events;
using CRUD.Domain.Todos.Exceptions;

namespace CRUD.Domain.Todos.Models
{
    public sealed class Todo : Entity, IAggregateRoot
    {
        protected Todo()
        {
        }

        public Todo(Guid id, string title, string description, DateTimeOffset? isCompletedAt, DateTimeOffset? isClosedAt, Guid? clientid) : base(id)
        {
            Title = title;
            Description = description;
            IsCompletedAt = isCompletedAt;
            IsClosedAt = isClosedAt;
            ClientId = clientid;
        }

        public Todo(string title, string description, DateTimeOffset? isCompletedAt, DateTimeOffset? isClosedAt, Guid? clientId)
        {
            Title = title;
            Description = description;
            IsCompletedAt = isCompletedAt;
            IsClosedAt = isClosedAt;
            ClientId = clientId;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTimeOffset? IsCompletedAt { get; private set; }

        public DateTimeOffset? IsClosedAt { get; private set; }

        public Guid? ClientId { get; private set; }

        public bool IsCompleted { get => IsCompletedAt != null; }

        public bool IsClosed { get => IsClosedAt != null; }

        public void Complete()
        {
            if (ClientId == null)
                throw new NeedHaveClientTodoException(nameof(ClientId));

            IsCompletedAt = DateTimeOffset.UtcNow;

            var @event = new CompletedTodoEvent(this.Id);
            AddEvent(@event);
        }

        public void Incomplete()
        {
            IsCompletedAt = null;

            var @event = new IncompletedTodoEvent(this.Id);
            AddEvent(@event);
        }

        public void Close()
        {
            IsClosedAt = DateTimeOffset.UtcNow;

            var @event = new CloseTodoEvent(this.Id);
            AddEvent(@event);
        }

        public void Update(string title, string description, Guid? clientId)
        {
            Title = title;
            Description = description;
            ClientId = clientId;
            UpdatedAt = DateTimeOffset.UtcNow;

            var @event = new UpdatedTodoEvent(Id, title, description, clientId);
            AddEvent(@event);
        }

        public void Delete()
        {
            DeletedAt = DateTimeOffset.UtcNow;

            var @event = new DeletedTodoEvent(Id);
            AddEvent(@event);
        }

        public static Todo Create(string title, string description, Guid? clientId)
        {
            var todo = new Todo(title, description, null, null, clientId);
            var @event = new CreatedTodoEvent(todo.Id, title, description, clientId);
            todo.AddEvent(@event);
            return todo;
        }
    }
}
