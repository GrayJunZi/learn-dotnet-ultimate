using Microsoft.AspNetCore.Mvc;

namespace PartialViewsExample.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        ViewData["ListTitle"] = "Cities";
        ViewData["Cities"] = new[] { "Beijing", "Shanghai", "Guangzhou", "Shenzhen" };
        return View();
    }

    [Route("/about")]
    public IActionResult About()
    {
        return View();
    }
}