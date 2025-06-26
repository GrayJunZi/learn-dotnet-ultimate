using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ActionFilters;

public class PersonsListActionFilter(ILogger<PersonsListActionFilter> logger) :IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation($"{nameof(PersonsListActionFilter)} OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogInformation($"{nameof(PersonsListActionFilter)} OnActionExecuted");
    }
}