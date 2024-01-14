using CRUD.Domain.Commoms;

namespace CRUD.Domain.Clients.Events
{
    public sealed class CreatedClientEvent : Event
    {
        public CreatedClientEvent()
        {
            Name = default!;
            Email = default!;
            Profession = default!;
        }

        public CreatedClientEvent(Guid aggregateId, string name, string email, string profession) : base(aggregateId)
        {
            Name = name;
            Email = email;
            Profession = profession;
        }


        public string Name { get; }

        public string Email { get; }

        public string Profession { get; }
    }
}
