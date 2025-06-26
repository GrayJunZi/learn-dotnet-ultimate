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

    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsListActionFilter), nameof(OnActionExecuting));
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsListActionFilter), nameof(OnActionExecuted));
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        logger.LogInformation("{FilterName}.{MethodName} Before", nameof(PersonsListActionFilter), nameof(OnActionExecutionAsync));
        await next();
        logger.LogInformation("{FilterName}.{MethodName} After", nameof(PersonsListActionFilter), nameof(OnActionExecutionAsync));
        context.HttpContext.Response.Headers.Add(key, value);
    }
}