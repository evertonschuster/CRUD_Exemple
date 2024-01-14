using CRUD.Domain.Commoms;

namespace CRUD.Domain.Todos.Events
{
    public sealed class CompletedTodoEvent : Event
    {
        public CompletedTodoEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
