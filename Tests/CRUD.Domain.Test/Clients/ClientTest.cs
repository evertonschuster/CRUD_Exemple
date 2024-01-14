using CRUD.Domain.Clients.Events;
using CRUD.Domain.Clients.Models;

namespace CRUD.Domain.Test.Clients
{
    public sealed class ClientTest
    {
        [Fact]
        public void Should_CreateClient_When_ValidDataIsProvided()
        {
            var name = new Name("Valid Name");
            var email = new Email("test@example.com");
            var profession = "Developer";


            var client = Client.Create(name, email, profession);


            Assert.Equal(name, client.Name);
            Assert.Equal(email, client.Email);
            Assert.Equal(profession, client.Profession);
            Assert.Collection(client.Events, @event =>
            {
                Assert.IsType<CreatedClientEvent>(@event);
                Assert.Equal(name, ((CreatedClientEvent)@event).Name);
                Assert.Equal(email, ((CreatedClientEvent)@event).Email);
                Assert.Equal(profession, ((CreatedClientEvent)@event).Profession);
            });
        }

        [Fact]
        public void Should_UpdateClient_When_ValidDataIsProvided()
        {
            var name = new Name("Valid Name");
            var email = new Email("test@example.com");
            var profession = "Developer";

            var client = Client.Create(name, email, profession);
            var newEmail = new Email("updated@example.com");
            var newProfession = "Updated Developer";


            client.Update(newEmail, newProfession);


            Assert.Equal(newEmail, client.Email);
            Assert.Equal(newProfession, client.Profession);
            Assert.Collection(client.Events,
            @event =>
            {
                Assert.IsType<CreatedClientEvent>(@event);
            },
            @event =>
            {
                Assert.IsType<UpdatedClientEvent>(@event);
                Assert.Equal(newEmail, ((UpdatedClientEvent)@event).Email);
                Assert.Equal(newProfession, ((UpdatedClientEvent)@event).Profession);
            });
        }

        [Fact]
        public void Should_DeleteClient()
        {
            var name = new Name("Valid Name");
            var email = new Email("test@example.com");
            var profession = "Developer";
            var client = Client.Create(name, email, profession);


            client.Delete();


            Assert.NotNull(client.DeletedAt);
            Assert.Collection(client.Events,
            @event =>
            {
                Assert.IsType<CreatedClientEvent>(@event);
            },
            @event =>
            {
                Assert.IsType<DeletedClientEvent>(@event);
            });
        }
    }
}
