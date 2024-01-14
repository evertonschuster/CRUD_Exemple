using Microsoft.EntityFrameworkCore;
using CRUD.Domain.Clients.Models;
using CRUD.Domain.Todos.Models;
using CRUD.infrastructure.Converters;

namespace CRUD.infrastructure
{
    public sealed class CRUDContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Todo> Todos { get; set; }

        public CRUDContext(DbContextOptions<CRUDContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasQueryFilter(b => b.DeletedAt == null);

            modelBuilder.Entity<Client>()
                .Property(e => e.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .HasMaxLength(100);
            modelBuilder.Entity<Client>()
                .Property(e => e.Profession)
                .HasMaxLength(100);


            modelBuilder.Entity<Todo>()
                .HasQueryFilter(b => b.DeletedAt == null);
            modelBuilder.Entity<Todo>()
                .Property(e => e.Title)
                .HasMaxLength(100);
            modelBuilder.Entity<Todo>()
                .Property(e => e.Description)
                .HasMaxLength(100);
            modelBuilder.Entity<Todo>()
                .HasOne<Client>()
                .WithMany();





            //TODO: Recording events in the database is not yet supported
            modelBuilder.Entity<Client>()
                .Ignore(e => e.Events);
            modelBuilder.Entity<Client>()
                .Ignore(e => e.Notificacoes);

            modelBuilder.Entity<Todo>()
                .Ignore(e => e.Events);
            modelBuilder.Entity<Todo>()
                .Ignore(e => e.Notificacoes);


            modelBuilder.Owned<Name>();
            modelBuilder.Owned<Email>();


        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<Email>()
                .HaveConversion<EmailConverter>();

            configurationBuilder.Properties<Name>()
                .HaveConversion<NameConverter>();
        }
    }
}
