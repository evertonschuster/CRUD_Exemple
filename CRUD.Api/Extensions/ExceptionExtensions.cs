using Microsoft.AspNetCore.Diagnostics;

namespace CRUD.Api.Extensions
{
    public static class ExceptionExtensions
    {
        public static void UseExceptionHandlerApp(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async context =>
                {
                    var exceptionHandler = context.RequestServices.GetRequiredService<IExceptionHandler>();
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionFeature != null)
                    {
                        await exceptionHandler.TryHandleAsync(context, exceptionFeature.Error, CancellationToken.None);
                    }
                }
            });
        }
    }
}
