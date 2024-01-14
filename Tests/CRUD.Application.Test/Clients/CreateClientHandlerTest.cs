using CRUD.Application.Clients.CreateClient;
using CRUD.Domain.Clients.Exceptions;
using CRUD.Domain.Clients.Models;
using CRUD.Domain.Clients.Repositories;
using CRUD.Domain.Commoms;

namespace CRUD.Application.Test.Clients
{
    public sealed class CreateClientHandlerTest
    {

        [Fact]
        public async Task Handle_ShouldCreateClient_When_ValidCommandIsProvided()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var command = new CreateClientCommand("Valid Name", "test@example.com", "Developer");
            var handler = new CreateClientHandler(clientRepository, unitOfWork);


            var result = await handler.Handle(command, CancellationToken.None);


            result.Id.Should().NotBeEmpty();
            clientRepository.Received(1).Add(Arg.Any<Client>());
            unitOfWork.Received(1).SaveChanges();
        }

        [Fact]
        public async Task Handle_ShouldCreateClient_When_EmptyNameCommandIsProvided()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var command = new CreateClientCommand("", "test@example.com", "Developer");
            var handler = new CreateClientHandler(clientRepository, unitOfWork);


            Func<Task> act = async () =>
            {
                await handler.Handle(command, CancellationToken.None); ;
            };


            await act.Should().ThrowAsync<NameNullOrWhitespaceException>();
        }

        [Fact]
        public async Task Handle_ShouldCreateClient_When_EmptyEmailCommandIsProvided()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var command = new CreateClientCommand("Valid Name", "", "Developer");
            var handler = new CreateClientHandler(clientRepository, unitOfWork);


            Func<Task> act = async () =>
            {
                await handler.Handle(command, CancellationToken.None); ;
            };


            await act.Should().ThrowAsync<EmailNullOrWhitespaceException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowOperationCanceled_When_CancellationTokenIsCancelled()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();

            var command = new CreateClientCommand("Valid Name", "test@example.com", "Developer");
            var handler = new CreateClientHandler(clientRepository, unitOfWork);
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();


            Func<Task> act = async () => await handler.Handle(command, cancellationTokenSource.Token);


            await act.Should().ThrowAsync<OperationCanceledException>();
            unitOfWork.DidNotReceive().SaveChanges();
            await unitOfWork.DidNotReceive().SaveChangesAsync();
        }

        [Fact]
        public void ToModel_Should_Return_Client_With_Same_Properties_As_Command()
        {
            var command = new CreateClientCommand("Test Name", "test@example.com", "Developer");


            var model = command.ToModel();


            model.Should().BeOfType<Client>();
            model.Name.ToString().Should().Be(command.Name);
            model.Email.ToString().Should().Be(command.Email);
            model.Profession.Should().Be(command.Profession);
        }
    }
}
