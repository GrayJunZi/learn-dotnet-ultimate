using Microsoft.AspNetCore.Mvc;
using ViewComponentsExample.Models;

namespace ViewComponentsExample.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/about")]
    public IActionResult About()
    {
        return View();
    }

    [Route("friends-list")]
    public IActionResult FriendsList()
    {
        var model = new PersonGridModel()
        {
            GridTitle = "Friends List",
            Persons = new List<Person>()
            {
                new Person()
                {
                    Name = "Mia",
                    JobTitle = "Developer",
                },
                new Person()
                {
                    Name = "Emma",
                    JobTitle = "UI",
                },
                new Person()
                {
                    Name = "Avva",
                    JobTitle = "QA"
                }
            }
        };
        return ViewComponent("Grid",  new
        {
            grid = model
        });
    }
}