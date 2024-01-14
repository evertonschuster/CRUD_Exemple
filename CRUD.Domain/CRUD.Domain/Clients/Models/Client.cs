using CRUD.Domain.Clients.Events;
using CRUD.Domain.Commoms;

namespace CRUD.Domain.Clients.Models
{
    public sealed class Client : Entity, IAggregateRoot
    {
        protected Client()
        {
        }

        public Client(Guid id, Name name, Email email, string profession) : base(id)
        {
            Name = name;
            Email = email;
            Profession = profession;
        }

        public Client(Name name, Email email, string profession)
        {
            Name = name;
            Email = email;
            Profession = profession;
        }

        public Name Name { get; private set; }

        public Email Email { get; private set; }

        public string Profession { get; private set; }


        /// <summary>
        /// Update the client's email and profession
        /// </summary>
        /// <param name="email"></param>
        /// <param name="profession"></param>
        public void Update(Email email, string profession)
        {
            Email = email;
            Profession = profession;
            UpdatedAt = DateTime.UtcNow;

            var @event = new UpdatedClientEvent(Id, email, profession);
            AddEvent(@event);
        }

        /// <summary>
        /// Delete the client
        /// </summary>
        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;

            var @event = new DeletedClientEvent(Id);
            AddEvent(@event);
        }

        /// <summary>
        /// factory for Create a new client
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="profession"></param>
        /// <returns></returns>
        public static Client Create(Name name, Email email, string profession)
        {
            var model = new Client(name, email, profession);
            var @event = new CreatedClientEvent(model.Id, name, email, profession);
            model.AddEvent(@event);

            return model;
        }
    }
}
