using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;

namespace CRUDExample.Filters.ActionFilters;

public class PersonsListActionFilter(ILogger<PersonsListActionFilter> logger) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation($"{nameof(PersonsListActionFilter)} OnActionExecuting");

        if (context.ActionArguments.ContainsKey("searchBy"))
        {
            var searchBy = context.ActionArguments["searchBy"] as string;
            if (!string.IsNullOrEmpty(searchBy))
            {
                var searchByOptions = new List<string>
                {
                    nameof(PersonResponse.PersonName),
                    nameof(PersonResponse.Email),
                    nameof(PersonResponse.DateOfBirth),
                    nameof(PersonResponse.Gender),
                    nameof(PersonResponse.CountryId),
                    nameof(PersonResponse.Address),
                };

                if (!searchByOptions.Contains(searchBy))
                {
                    logger.LogInformation("SearchBy actual value {searchBy}", searchBy);
                    context.ActionArguments["searchBy"] = searchByOptions[2];
                    logger.LogInformation("SearchBy update value {searchBy}", context.ActionArguments["searchBy"]);
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogInformation($"{nameof(PersonsListActionFilter)} OnActionExecuted");
    }
}