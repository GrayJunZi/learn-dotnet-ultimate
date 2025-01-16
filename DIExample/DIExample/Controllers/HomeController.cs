using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace DIExample.Controllers;

public class HomeController : Controller
{
    private readonly ICitiesService _citiesService;

    public HomeController(ICitiesService citiesService)
    {
        _citiesService = citiesService; 
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