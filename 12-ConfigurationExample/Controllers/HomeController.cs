using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ApiOptions _apiOptions;

    public HomeController(
        IConfiguration configuration,
        IOptions<ApiOptions> options)
    {
        _configuration = configuration;
        _apiOptions = options.Value;
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
        ViewBag.ClientID = bindApiOptions.ClientID;
        ViewBag.ClientSecret = bindApiOptions.ClientSecret;
        
        // 方式 7
        ViewBag.ClientID = _apiOptions.ClientID;
        ViewBag.ClientSecret = _apiOptions.ClientSecret;
        
        return View();
    }
}