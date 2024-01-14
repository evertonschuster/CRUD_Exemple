namespace CRUD.Application.Clients.GetByIdClient
{
    public sealed class GetByIdClientQuery : IRequest<GetByIdClientResult>
    {
        public GetByIdClientQuery()
        {

        }

        public GetByIdClientQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
