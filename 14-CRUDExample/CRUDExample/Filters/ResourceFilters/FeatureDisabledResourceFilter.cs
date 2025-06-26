using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResourceFilters;

public class FeatureDisabledResourceFilter(ILogger<FeatureDisabledResourceFilter> logger, bool isDisabled = true)
    : IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        logger.LogInformation("{FilterName}.{MethodName} Before", nameof(FeatureDisabledResourceFilter),
            nameof(OnResourceExecutionAsync));

        if (isDisabled)
        {
            context.Result = new StatusCodeResult(501);
            return;
        }
        
        await next();
        
        logger.LogInformation("{FilterName}.{MethodName} After", nameof(FeatureDisabledResourceFilter),
            nameof(OnResourceExecutionAsync));
    }
}