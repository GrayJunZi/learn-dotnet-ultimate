using Microsoft.AspNetCore.Mvc;
using Services;

namespace DIExample.Controllers;

public class HomeController : Controller
{
    private readonly CitiesService _citiesService;

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