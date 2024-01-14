using CRUD.Domain.Clients.Exceptions;
using CRUD.Domain.Clients.Repositories;
using CRUD.Domain.Commoms;

namespace CRUD.Application.Clients.DeleteClient
{
    public sealed class DeleteClientHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteClientCommand>
    {
        private readonly IClientRepository clientRepository = clientRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var model = clientRepository.GetById(request.Id) ?? throw new ClientNotFoundException();

            model.Delete();
            clientRepository.Update(model);
            cancellationToken.ThrowIfCancellationRequested();
            unitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
