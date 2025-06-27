using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResourceFilters.AuthorizationFilters;

public class TokenAuthorizationFilter : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            return;
        }
    }
}