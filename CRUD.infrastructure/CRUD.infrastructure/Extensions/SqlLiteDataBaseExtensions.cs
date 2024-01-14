using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CRUD.Domain.Commoms;
using CRUD.infrastructure.UnitOfWork;

namespace CRUD.infrastructure.Extensions
{
    public static class SqlLiteDataBaseExtensions
    {
        public static void MigrateApplicationContext(this DbContext context)
        {
            context.Database.Migrate();
        }

        public static void AddLocalApplicationContext(this IServiceCollection services)
        {
            var folder = Environment.SpecialFolder.ApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, ".db");
            var connectionString = $"Data Source={dbPath}";

            services.AddDbContextPool<CRUDContext>(options =>
                options.UseSqlite(connectionString)
            );

            services.AddScoped<IUnitOfWork, UnitOfWorkEfCore>();
        }
    }
}
