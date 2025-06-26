using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResultFilters;

public class PersonsListResultFilter(ILogger<PersonsListResultFilter> logger) : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        logger.LogInformation("{FilterName}.{MethodName} Before", nameof(PersonsListResultFilter), nameof(OnResultExecutionAsync));
        await next();
        logger.LogInformation("{FilterName}.{MethodName} After", nameof(PersonsListResultFilter), nameof(OnResultExecutionAsync));

        context.HttpContext.Response.Headers["Last-Modified"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}