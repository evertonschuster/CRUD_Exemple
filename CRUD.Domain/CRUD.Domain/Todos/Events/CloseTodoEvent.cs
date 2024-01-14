using CRUD.Domain.Commoms;

namespace CRUD.Domain.Todos.Events
{
    public sealed class CloseTodoEvent : Event
    {
        public CloseTodoEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
