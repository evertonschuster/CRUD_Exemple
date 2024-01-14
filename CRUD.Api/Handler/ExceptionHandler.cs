using Microsoft.AspNetCore.Diagnostics;
using CRUD.Domain.Commoms;

namespace CRUD.Api.Handler
{
    internal class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            this.logger = logger;
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}", exception.Message, DateTime.UtcNow);

            var dictionary = new Dictionary<string, string[]>();

            if (exception is BusinessExeption businessException)
            {
                dictionary.Add(businessException.Property ?? string.Empty, [businessException.Message]);
            }
            else
            {
                dictionary.Add(string.Empty, ["Internal Server Error"]);
            }

            var errors = new HttpValidationProblemDetails(dictionary);
            httpContext.Response.WriteAsJsonAsync(errors, cancellationToken: cancellationToken).Wait(cancellationToken);

            return ValueTask.FromResult(true);
        }
    }
}
