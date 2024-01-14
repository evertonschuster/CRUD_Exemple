using CRUD.Domain.Clients.Models;

namespace CRUD.Application.Clients.GetByIdClient
{
    public sealed class GetByIdClientResult
    {
        public GetByIdClientResult(Client model)
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
