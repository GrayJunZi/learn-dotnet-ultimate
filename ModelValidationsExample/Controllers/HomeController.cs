using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers;

public class HomeController : Controller
{
    [Route("register")]
    public IActionResult Register(Person person)
    {
        return Content($"{person}");
    }
}