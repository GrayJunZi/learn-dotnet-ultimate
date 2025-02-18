using Microsoft.AspNetCore.Mvc;

namespace EnvironmentsExample.Controllers;

public class PersonsController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PersonsController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [Route("/")]
    [Route("persons/index")]
    public IActionResult Index()
    {
       ViewBag.EnvironmentName = _webHostEnvironment.EnvironmentName;
        return View();
    }

    [Route("/about")]
    public IActionResult About()
    {
        return View();
    }
}