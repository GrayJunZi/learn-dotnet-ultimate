using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers;

public class HomeController : Controller
{
    [Route("register")]
    public IActionResult Register([FromBody] Person person,[FromHeader(Name = "User-Agent")] string userAgent)
    {
        if (ModelState.IsValid)
        {
            var errors = string.Join(',', ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
            return BadRequest(errors);
        }

        var key = ControllerContext.HttpContext.Response.Headers["key"];

        return Content($"{person}, {userAgent}");
    }
}