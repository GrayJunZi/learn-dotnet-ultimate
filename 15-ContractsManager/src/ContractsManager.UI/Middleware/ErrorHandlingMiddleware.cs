using Serilog;

namespace CRUDExample.Middleware;

public class ErrorHandlingMiddleware(
    RequestDelegate next,
    ILogger<ErrorHandlingMiddleware> logger,
    IDiagnosticContext diagnosticContext)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                logger.LogError(ex, "{ExceptionType} {ExceptionMessage}",
                    ex.InnerException.GetType().ToString(),
                    ex.InnerException.Message);
            }
            else
            {
                logger.LogError(ex, "{ExceptionType} {ExceptionMessage}",
                    ex.GetType().ToString(),
                    ex.Message);
            }

            await context.Response.WriteAsync("Error occured");
        }
    }
}