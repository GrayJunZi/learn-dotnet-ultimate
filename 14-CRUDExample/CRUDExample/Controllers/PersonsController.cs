using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace EnvironmentsExample.Controllers;

public class PersonsController : Controller
{
    private readonly IPersonsService _personsService;
    private readonly ICountriesService _countriesService;

    public PersonsController(
        IPersonsService personsService,
        ICountriesService countriesService)
    {
        _personsService = personsService;
        _countriesService = countriesService;
    }

    [Route("/")]
    [Route("persons/index")]
    public IActionResult Index(
        string? field = null,
        string? value = null,
        string sortBy = nameof(PersonResponse.PersonName),
        SortOptions? sortOrder = SortOptions.Asc)
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
        };
        ViewBag.CurrentField = field;
        ViewBag.CurrentValue = value;

        var persons = _personsService.GetFilteredPersons(field, value);
        persons = _personsService.GetSortedPersons(persons, sortBy, sortOrder);
        ViewBag.CurrentSortBy = sortBy;
        ViewBag.CurrentSortOrder = sortOrder;
        
        return View(persons);
    }

    [Route("/persons/create")]
    [HttpGet]
    public IActionResult Create()
    {
       ViewBag.Countries = _countriesService.GetAllCountries();
        return View();
    }

    [Route("/persons/create")]
    [HttpPost]
    public IActionResult Create(PersonAddRequest personAddRequest)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Countries = _countriesService.GetAllCountries();
            ViewBag.Errors  = ModelState.Values.SelectMany(v => v.Errors).Select(e=>e.ErrorMessage).ToList();
            return View();
        }
        
        _personsService.AddPerson(personAddRequest);
        return RedirectToAction("Index","Persons");
    }
}