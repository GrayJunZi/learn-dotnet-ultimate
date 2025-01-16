using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace DIExample.Controllers;

public class HomeController : Controller
{
    private readonly ICitiesService _citiesService;

    public HomeController()
    {
        _citiesService = new CitiesService();
    }
    
    [Route("/")]
    public IActionResult Index()
    {
        return View(_citiesService.GetCities());
    }

    [Route("/about")]
    public IActionResult About()
    {
        return View();
    }
}