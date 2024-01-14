using CRUD.Domain.Commoms;

namespace CRUD.Domain.Todos.Events
{
    public sealed class DeletedTodoEvent : Event
    {
        public DeletedTodoEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
