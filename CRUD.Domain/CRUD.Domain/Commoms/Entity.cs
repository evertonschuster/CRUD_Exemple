using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Domain.Commoms
{
    public class Entity
    {
        [Key]
        public Guid Id { get; protected set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; protected set; }
        public DateTimeOffset? DeletedAt { get; protected set; }

        [NotMapped]
        private readonly List<Event> _events;

        [NotMapped]
        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            _events = [];
        }

        protected Entity(Guid? id = null, DateTimeOffset? createdAt = null, DateTimeOffset? updatedAt = null, List<Event>? events = null)
        {
            Id = id ?? Guid.NewGuid();
            CreatedAt = createdAt ?? DateTimeOffset.UtcNow;
            UpdatedAt = updatedAt;
            _events = events ?? [];
        }

        [NotMapped]
        public IReadOnlyCollection<Event> Notificacoes => _events.AsReadOnly();

        protected void AddEvent(Event @event)
        {
            _events.Add(@event);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
