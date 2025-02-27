using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace EnvironmentsExample.Controllers;

public class PersonsController : Controller
{
    private readonly IPersonsService _personsService;

    public PersonsController(IPersonsService personsService)
    {
        _personsService = personsService;
    }

    [Route("/")]
    [Route("persons/index")]
    public IActionResult Index()
    {
        var persons = _personsService.GetAllPersons();
        return View(persons);
    }

    [Route("/about")]
    public IActionResult About()
    {
        return View();
    }
}