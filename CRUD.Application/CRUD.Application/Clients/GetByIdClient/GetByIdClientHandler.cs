using CRUD.Domain.Clients.Exceptions;
using CRUD.Domain.Clients.Repositories;

namespace CRUD.Application.Clients.GetByIdClient
{
    public sealed class GetByIdClientHandler(IClientRepository clientRepository) : IRequestHandler<GetByIdClientQuery, GetByIdClientResult>
    {
        private readonly IClientRepository clientRepository = clientRepository;

        public Task<GetByIdClientResult> Handle(GetByIdClientQuery request, CancellationToken cancellationToken)
        {
            var model = clientRepository.GetById(request.Id) ?? throw new ClientNotFoundException();
            var result = new GetByIdClientResult(model);

            return Task.FromResult(result);
        }
    }
}