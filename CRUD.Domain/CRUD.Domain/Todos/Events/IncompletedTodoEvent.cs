using CRUD.Domain.Commoms;

namespace CRUD.Domain.Todos.Events
{
    public sealed class IncompletedTodoEvent : Event
    {
        public IncompletedTodoEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
