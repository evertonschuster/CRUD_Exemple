namespace CRUD.Domain.Commoms
{
    public class Event
    {
        public Guid Id { get; protected set; }

        public DateTimeOffset Timestamp { get; }

        public Guid AggregateId { get; }

        protected Event()
        {
        }

        protected Event(Guid aggregateId)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTimeOffset.Now;
            AggregateId = aggregateId;
        }
    }
}
