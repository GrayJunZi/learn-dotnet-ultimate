using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ActionFilters;

public class ResponseHeaderActionFilter(
    ILogger<ResponseHeaderActionFilter> logger,
    string key,
    string value,
    int order)
    : IActionFilter, IOrderedFilter
{
    public int Order { get; } = order;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsListActionFilter), nameof(OnActionExecuting));
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsListActionFilter), nameof(OnActionExecuted));

        context.HttpContext.Response.Headers.Add(key, value);
    }
}