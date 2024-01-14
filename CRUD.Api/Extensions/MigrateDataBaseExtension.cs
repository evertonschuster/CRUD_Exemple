using CRUD.infrastructure;
using CRUD.infrastructure.Extensions;

namespace CRUD.Api.Extensions
{
    public static class MigrateDataBaseExtension
    {
        public static void MigrateApplicationDataBase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<CRUDContext>();
            context?.MigrateApplicationContext();
        }
    }
}
