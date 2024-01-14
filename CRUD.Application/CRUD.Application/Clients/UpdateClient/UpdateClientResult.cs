using CRUD.Domain.Clients.Models;

namespace CRUD.Application.Clients.UpdateClient
{
    public record UpdateClientResult
    {
        public UpdateClientResult(Client model)
        {
            Id = model.Id;
            Name = model.Name;
            Email = model.Email;
            Profession = model.Profession;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Email { get; }

        public string Profession { get; }
    }
}
