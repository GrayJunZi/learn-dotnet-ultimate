using CRUDExample.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;

namespace CRUDExample.Filters.ActionFilters;

public class PersonsListActionFilter(ILogger<PersonsListActionFilter> logger) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsListActionFilter),nameof(OnActionExecuting));

        context.HttpContext.Items["arguments"] = context.ActionArguments;

        if (context.ActionArguments.ContainsKey("field"))
        {
            var field = context.ActionArguments["field"] as string;
            if (!string.IsNullOrEmpty(field))
            {
                var fieldOptions = new List<string>
                {
                    nameof(PersonResponse.PersonName),
                    nameof(PersonResponse.Email),
                    nameof(PersonResponse.DateOfBirth),
                    nameof(PersonResponse.Gender),
                    nameof(PersonResponse.CountryId),
                    nameof(PersonResponse.Address),
                };

                if (!fieldOptions.Contains(field))
                {
                    logger.LogInformation("Field actual value {field}", field);
                    context.ActionArguments["field"] = fieldOptions[2];
                    logger.LogInformation("Field update value {field}", context.ActionArguments["field"]);
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogInformation("{FilterName}.{MethodName}", nameof(PersonsListActionFilter),nameof(OnActionExecuted));

        var personsController = (PersonsController)context.Controller;
        var arguments = (IDictionary<string, object?>?)context.HttpContext.Items["arguments"];
        if (arguments != null)
        {
            if (arguments.ContainsKey("field"))
                personsController.ViewData["field"] = arguments["field"];
            
            if (arguments.ContainsKey("value"))
                personsController.ViewData["value"] = arguments["value"];
            
            if (arguments.ContainsKey("sortBy"))
                personsController.ViewData["sortBy"] = arguments["sortBy"];
            
            if (arguments.ContainsKey("sortOrder"))
                personsController.ViewData["sortOrder"] = arguments["sortOrder"];
        }
    }
}