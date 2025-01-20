using Microsoft.AspNetCore.Mvc;

namespace ConfigurationExample.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [Route("/")]
    public IActionResult Index()
    {
        ViewBag.MyKey = _configuration["MyKey"];
        ViewBag.x = _configuration.GetValue<int>("x");
        ViewBag.apiKey = _configuration.GetValue<string>("apiKey", "the default API Key");
        return View();
    }
}