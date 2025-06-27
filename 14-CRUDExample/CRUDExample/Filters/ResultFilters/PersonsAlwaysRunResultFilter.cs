using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResultFilters;

public class PersonsAlwaysRunResultFilter(ILogger<PersonsListResultFilter> logger) : IAlwaysRunResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsAlwaysRunResultFilter),
            nameof(OnResultExecuting));
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsAlwaysRunResultFilter),
            nameof(OnResultExecuted));
    }
}