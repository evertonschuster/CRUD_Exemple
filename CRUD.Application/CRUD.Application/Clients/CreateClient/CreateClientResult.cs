namespace CRUD.Application.Clients.CreateClient
{
    public record CreateClientResult
    {
        public CreateClientResult(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }

    }
}
