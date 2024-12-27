using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.CustomModelBinders;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers;

public class HomeController : Controller
{
    [Route("register")]
    public IActionResult Register([FromBody] [ModelBinder(BinderType = typeof(PersonModelBinder))] Person person)
    {
        if (ModelState.IsValid)
        {
            var errors = string.Join(',', ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
            return BadRequest(errors);
        }

        return Content($"{person}");
    }
}