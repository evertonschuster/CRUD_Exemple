using CRUD.Domain.Clients.Models;

namespace CRUD.Application.Clients.CreateClient
{
    public record CreateClientCommand : IRequest<CreateClientResult>
    {
        public CreateClientCommand()
        {
            Name = string.Empty;
            Email = string.Empty;
            Profession = string.Empty;
        }

        public CreateClientCommand(string name, string email, string profession)
        {
            Name = name;
            Email = email;
            Profession = profession;
        }

        /// <summary>
        /// Name of the client
        /// </summary>
        /// <example>John Doe</example> 
        public string Name { get; set; }

        /// <summary>
        /// Email of the client
        /// </summary>
        /// <example>example@example.com</example>
        public string Email { get; set; }


        /// <summary>
        /// Profession of the client
        /// </summary>
        /// <example>Developer</example>
        public string Profession { get; set; }


        public Client ToModel()
        {
            return Client.Create(Name, Email, Profession);
        }
    }
}
