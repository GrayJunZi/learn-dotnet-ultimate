using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace DIExample.Controllers;

public class HomeController : Controller
{
    private readonly ICitiesService _citiesService1;
    private readonly ICitiesService _citiesService2;
    private readonly ICitiesService _citiesService3;

    public HomeController(
        ICitiesService citiesService1,
        ICitiesService citiesService2,
        ICitiesService citiesService3)
    {
        _citiesService1 = citiesService1;
        _citiesService2 = citiesService2;
        _citiesService3 = citiesService3; 
    }
    
    [Route("/")]
    public IActionResult Index([FromServices] ICitiesService citiesService)
    {
        ViewBag.Instances = new Guid[] { _citiesService1.InstanceId, _citiesService2.InstanceId, _citiesService3.InstanceId };
        
        return View(citiesService.GetCities());
    }

    [Route("/about")]
    public IActionResult About()
    {
        return View();
    }
}