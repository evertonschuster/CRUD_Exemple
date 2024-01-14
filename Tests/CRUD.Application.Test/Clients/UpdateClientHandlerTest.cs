using CRUD.Application.Clients.UpdateClient;
using CRUD.Domain.Clients.Exceptions;
using CRUD.Domain.Clients.Models;
using CRUD.Domain.Clients.Repositories;
using CRUD.Domain.Commoms;

namespace CRUD.Application.Test.Clients
{
    public sealed class UpdateClientHandlerTest
    {
        [Fact]
        public async Task Handle_Should_UpdateClient_When_ValidCommandIsProvided()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var handler = new UpdateClientHandler(clientRepository, unitOfWork);
            var command = new UpdateClientCommand(Guid.NewGuid(), "test@example.com", "Developer");
            var client = new Client(command.Id, "Everton", "everton@gmail.com", "Dev");


            clientRepository.GetById(command.Id).Returns(client);
            var result = await handler.Handle(command, CancellationToken.None);



            clientRepository.Received(1).GetById(command.Id);
            clientRepository.Received(1).Update(client);
            unitOfWork.Received(1).SaveChanges();
            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.Email.Should().Be(command.Email);
            result.Profession.Should().Be(command.Profession);
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_ClientNotFound()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var handler = new UpdateClientHandler(clientRepository, unitOfWork);
            var command = new UpdateClientCommand(Guid.NewGuid(), "test@example.com", "Developer");


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
        public async Task Handle_Should_ThrowOperationCanceled_When_CancellationTokenIsCancelled()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var client = new Client(Guid.NewGuid(), "Everton", "everton@gmail.com", "Dev");
            clientRepository.GetById(Arg.Any<Guid>()).Returns(client);

            var handler = new UpdateClientHandler(clientRepository, unitOfWork);
            var command = new UpdateClientCommand(Guid.NewGuid(), "test@example.com", "Developer");
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();


            Func<Task> act = async () => await handler.Handle(command, cancellationTokenSource.Token);


            await act.Should().ThrowAsync<OperationCanceledException>();
            unitOfWork.DidNotReceive().SaveChanges();
            await unitOfWork.DidNotReceive().SaveChangesAsync();
        }
    }
}
