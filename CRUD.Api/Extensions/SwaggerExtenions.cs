using System.Reflection;

namespace CRUD.Api.Extensions
{
    public static class SwaggerExtenions
    {
        public static void AddSwaggerGenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\PublicApi.xml");
                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\PublicApplication.xml");
            });
        }

        public static void UseSwaggerUIApi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
