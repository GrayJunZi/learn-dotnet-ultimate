using CRUDExample.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts;
using ServiceContracts.DTO;

namespace CRUDExample.Filters.ActionFilters;

public class PersonCreateAndEditPostActionFilter(ICountriesService countriesService) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.Controller is PersonsController controller)
        {
            if (!controller.ModelState.IsValid)
            {
                controller.ViewBag.Countries = await countriesService.GetAllCountries();
                controller.ViewBag.Errors =
                    controller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                
                var personAddRequest = context.ActionArguments["personAddRequest"] as PersonAddRequest;
                context.Result = controller.View(personAddRequest);
            }
            return;
        }

        await next();
    }
}