using Microsoft.AspNetCore.Mvc;
using ViewsExample.Models;

namespace ViewsExample.Controllers;

public class HomeController : Controller
{
    [Route("home")]
    [Route("/")]
    public IActionResult Index()
    {
        ViewData["Title"] = "Home";
        var persons = Enumerable.Range(1, 10).Select(x => new Person()
        {
            Name = "Robot T" + (3500 + x),
            DateOfBirth = DateTime.Now,
            Gender = Gender.Male
        });
        return View("Index", persons);
    }
}