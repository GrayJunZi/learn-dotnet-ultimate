using Microsoft.AspNetCore.Mvc;
using PartialViewsExample.Models;

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

    [Route("programming-languages")]
    public IActionResult ProgrammingLanguages()
    {
        var model = new ListModel
        {
            Title = "Programming Languages",
            List = new[] { "Java", "C#", "Python", "Go" },
        };
        return PartialView("_StronglyListPartialView", model);
    }
}