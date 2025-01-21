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
        // 方式 1
        ViewBag.MyKey = _configuration["MyKey"];
        
        // 方式 2
        ViewBag.x = _configuration.GetValue<int>("x");
        ViewBag.apiKey = _configuration.GetValue<string>("apiKey", "the default API Key");
        
        // 方式 3
        ViewBag.ClientID = _configuration["API:ClientID"];
        ViewBag.ClientSecret = _configuration.GetValue<string>("API:ClientSecret","the default client secret");
        
        // 方式 4
        var api = _configuration.GetSection("API");
        ViewBag.ClientID = api["ClientID"];
        ViewBag.ClientSecret = api["ClientSecret"];
        
        // 方式 5
        var apiOptions = _configuration.GetSection("API").Get<ApiOptions>();
        ViewBag.ClientID = apiOptions.ClientID;
        ViewBag.ClientSecret = apiOptions.ClientSecret;
        
        // 方式 6
        var bindApiOptions = new ApiOptions();
        _configuration.GetSection("API").Bind(bindApiOptions);
        
        return View();
    }
}