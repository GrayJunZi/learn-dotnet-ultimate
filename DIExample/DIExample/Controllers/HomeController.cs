using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace DIExample.Controllers;

public class HomeController : Controller
{
    private readonly ICitiesService _citiesService1;
    private readonly ICitiesService _citiesService2;
    private readonly ICitiesService _citiesService3;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HomeController(
        ICitiesService citiesService1,
        ICitiesService citiesService2,
        ICitiesService citiesService3,
        IServiceScopeFactory serviceScopeFactory)
    {
        _citiesService1 = citiesService1;
        _citiesService2 = citiesService2;
        _citiesService3 = citiesService3;
        _serviceScopeFactory = serviceScopeFactory;
    }

    [Route("/")]
    public IActionResult Index([FromServices] ICitiesService citiesService)
    {
        ViewBag.Instances = new List<Guid>
        {
            _citiesService1.InstanceId,
            _citiesService2.InstanceId,
            _citiesService3.InstanceId
        };

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scopeCitiesService = scope.ServiceProvider.GetService<ICitiesService>();
            ViewBag.Instances.Add(scopeCitiesService.InstanceId);
        }

        return View(citiesService.GetCities());
    }

    [Route("/about")]
    public IActionResult About()
    {
        return View();
    }
}