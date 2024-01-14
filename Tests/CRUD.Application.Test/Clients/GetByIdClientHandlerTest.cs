using CRUD.Application.Clients.GetByIdClient;
using CRUD.Domain.Clients.Exceptions;
using CRUD.Domain.Clients.Models;
using CRUD.Domain.Clients.Repositories;

namespace CRUD.Application.Test.Clients
{
    public sealed class GetByIdClientHandlerTest
    {
        [Fact]
        public async Task Handle_Should_Return_GetByIdClientResult_When_Client_Exists()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var client = new Client(Guid.NewGuid(), "Test", "test@test.com", "Developer");
            clientRepository.GetById(client.Id).Returns(client);

            var query = new GetByIdClientQuery(client.Id);
            var handler = new GetByIdClientHandler(clientRepository);


            var result = await handler.Handle(query, CancellationToken.None);


            result.Should().NotBeNull();
            result.Name.Should().Be(client.Name);
            result.Email.Should().Be(client.Email);
            result.Profession.Should().Be(client.Profession);
        }

        [Fact]
        public async Task Handle_Should_Throw_ClientNotFoundException_When_Client_Does_Not_Exist()
        {
            var clientRepository = Substitute.For<IClientRepository>();
            var query = new GetByIdClientQuery(Guid.NewGuid());
            var handler = new GetByIdClientHandler(clientRepository);


            async Task act()
            {
                await handler.Handle(query, CancellationToken.None);
            }


            await Assert.ThrowsAsync<ClientNotFoundException>(act);
        }
    }
}
