using CRUD.Application.Clients.DeleteClient;
using CRUD.Domain.Clients.Exceptions;
using CRUD.Domain.Clients.Models;
using CRUD.Domain.Clients.Repositories;
using CRUD.Domain.Commoms;

namespace CRUD.Application.Test.Clients
{
    public sealed class DeleteClientHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldDeleteClient_When_ValidCommandIsProvided()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var client = new Client("Test", "Test@teste.com", "Test");
            clientRepository.GetById(Arg.Any<Guid>()).Returns(client);

            var command = new DeleteClientCommand(Guid.NewGuid());
            var handler = new DeleteClientHandler(clientRepository, unitOfWork);


            await handler.Handle(command, CancellationToken.None);


            clientRepository.Received(1).GetById(Arg.Any<Guid>());
            clientRepository.Received(1).Update(Arg.Any<Client>());
            unitOfWork.Received(1).SaveChanges();
        }

        [Fact]
        public async Task Handle_ShouldThrowException_When_ClientNotFound()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var command = new DeleteClientCommand(Guid.NewGuid());
            var handler = new DeleteClientHandler(clientRepository, unitOfWork);


            Func<Task> act = async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            };


            await act.Should().ThrowAsync<ClientNotFoundException>();
            clientRepository.Received(1).GetById(Arg.Any<Guid>());
            clientRepository.Received(0).Delete(Arg.Any<Client>());
            unitOfWork.Received(0).SaveChanges();
        }


        [Fact]
        public async Task Handle_ShouldThrowOperationCanceled_When_CancellationTokenIsCancelled()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var client = new Client("Test", "Test@teste.com", "Test");
            clientRepository.GetById(Arg.Any<Guid>()).Returns(client);

            var command = new DeleteClientCommand(Guid.NewGuid());
            var handler = new DeleteClientHandler(clientRepository, unitOfWork);
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();


            Func<Task> act = async () => await handler.Handle(command, cancellationTokenSource.Token);


            await act.Should().ThrowAsync<OperationCanceledException>();
            unitOfWork.DidNotReceive().SaveChanges();
            await unitOfWork.DidNotReceive().SaveChangesAsync();
        }
    }
}
