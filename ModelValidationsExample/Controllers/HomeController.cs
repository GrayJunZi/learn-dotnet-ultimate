using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers;

public class HomeController : Controller
{
    [Route("register")]
    public IActionResult Register([Bind(nameof(Person.Name), nameof(Person.Email))] Person person)
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