using Microsoft.Extensions.DependencyInjection;
using CRUD.Domain.Clients.Repositories;
using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Repositories;
using CRUD.infrastructure.Clients;
using CRUD.infrastructure.Todos;
using CRUD.infrastructure.UnitOfWork;

namespace CRUD.infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static void AddInfraInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorkEfCore>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
        }
    }
}
