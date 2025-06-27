using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ActionFilters;

public class ResponseHeaderActionFilter(
    ILogger<ResponseHeaderActionFilter> logger,
    string key,
    string value,
    int order)
    : IAsyncActionFilter, IOrderedFilter
{
    public int Order { get; } = order;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        logger.LogInformation("{FilterName}.{MethodName} Before", nameof(PersonsListActionFilter),
            nameof(OnActionExecutionAsync));
        await next();
        logger.LogInformation("{FilterName}.{MethodName} After", nameof(PersonsListActionFilter),
            nameof(OnActionExecutionAsync));
        context.HttpContext.Response.Headers.Add(key, value);
    }
}