namespace CRUD.Application.Clients.UpdateClient
{
    public record UpdateClientCommand : IRequest<UpdateClientResult>
    {
        public UpdateClientCommand()
        {
            Email = string.Empty;
            Profession = string.Empty;
        }

        public UpdateClientCommand(Guid id, string email, string profession)
        {
            Id = id;
            Email = email;
            Profession = profession;
        }

        public Guid Id { get; set; }


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
    }
}
