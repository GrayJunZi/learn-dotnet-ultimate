using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

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
    public IActionResult Index(string? field = null, string? value = null)
    {
        ViewBag.SearchFields = new Dictionary<string, object>
        {
            { "", "All" },
            { nameof(PersonResponse.PersonName), "Person Name" },
            { nameof(PersonResponse.Email), "Email" },
            { nameof(PersonResponse.DateOfBirth), "Date Of Birth" },
            { nameof(PersonResponse.Age), "Age" },
            { nameof(PersonResponse.Country), "Country" },
            { nameof(PersonResponse.Address), "Address" },
            { nameof(PersonResponse.ReceiveNewsletter), "Receive Newsletter" },
        };

        var persons = _personsService.GetAllPersons();
        return View(persons);
    }

    [Route("/about")]
    public IActionResult About()
    {
        return View();
    }
}