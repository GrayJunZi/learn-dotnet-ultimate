using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace EnvironmentsExample.Controllers;

[Route("persons")]
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
    [Route("index")]
    public async Task<IActionResult> Index(
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

        var persons = await _personsService.GetFilteredPersons(field, value);
        persons = await _personsService.GetSortedPersons(persons, sortBy, sortOrder);
        ViewBag.CurrentSortBy = sortBy;
        ViewBag.CurrentSortOrder = sortOrder;

        return View(persons);
    }

    [Route("create")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Countries = (await _countriesService.GetAllCountries())
            .Select(x => new SelectListItem
            {
                Text = x.CountryName,
                Value = x.CountryId.ToString()
            });

        return View();
    }

    [Route("create")]
    [HttpPost]
    public IActionResult Create(PersonAddRequest personAddRequest)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Countries = _countriesService.GetAllCountries();
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return View();
        }

        _personsService.AddPerson(personAddRequest);
        return RedirectToAction("Index", "Persons");
    }

    [Route("edit/{personId}")]
    [HttpGet]
    public async Task<IActionResult> Edit(Guid personId)
    {
        var response = await _personsService.GetPersonByPersonId(personId);
        if (response == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Countries = (await _countriesService.GetAllCountries())
            .Select(x => new SelectListItem
            {
                Text = x.CountryName,
                Value = x.CountryId.ToString()
            });

        var personUpdateRequest = response.ToPersonUpdateRequest();
        return View(personUpdateRequest);
    }

    [Route("edit/{personId}")]
    [HttpPost]
    public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Countries = _countriesService.GetAllCountries();
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return View();
        }

        var person = _personsService.GetPersonByPersonId(personUpdateRequest.PersonId);
        if (person == null)
        {
            return RedirectToAction("Index");
        }

        _personsService.UpdatePerson(personUpdateRequest);
        return RedirectToAction("Index", "Persons");
    }

    [Route("delete/{personId}")]
    [HttpGet]
    public async Task<IActionResult> Delete(Guid? personId)
    {
        var response = await _personsService.GetPersonByPersonId(personId);
        if (response == null)
        {
            return RedirectToAction("Index");
        }

        return View(response);
    }

    [Route("delete/{personId}")]
    [HttpPost]
    public async Task<IActionResult> Delete(PersonUpdateRequest personUpdateRequest)
    {
        var person = await _personsService.GetPersonByPersonId(personUpdateRequest.PersonId);
        if (person == null)
        {
            return RedirectToAction("Index");
        }

        await _personsService.DeletePerson(personUpdateRequest.PersonId);
        return RedirectToAction("Index", "Persons");
    }

    [Route("PersonsPdf")]
    public async Task<IActionResult> PersonsPdf()
    {
        var persons = await _personsService.GetAllPersons();
        
        return new ViewAsPdf("PersonsPdf", persons,ViewData)
        {
            PageMargins = new Margins
            {
                Top = 20,
                Left = 20,
                Right = 20,
                Bottom = 20,
            },
            PageOrientation = Orientation.Landscape
        };
    }
}