namespace CRUD.Application.Clients.DeleteClient
{
    public record DeleteClientCommand : IRequest
    {
        public DeleteClientCommand()
        {

        }

        public DeleteClientCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
