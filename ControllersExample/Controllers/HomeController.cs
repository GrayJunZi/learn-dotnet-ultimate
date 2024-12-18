using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers;

public class HomeController : Controller
{
    [Route("home")]
    [Route("/")]
    public ContentResult Index()
    {
        return new ContentResult
        {
            Content = "<h1 align='center'>Hello World from Index!</h1>",
            ContentType = "text/html",
        };
    }

    [Route("about")]
    public ContentResult About()
    {
        return Content("Hello from About", "text/plain"); ;
    }

    [Route("contact-us/{mobile:regex(^\\d{{11}}$)}")]
    public string Contact()
    {
        return "Hello from Contact";
    }
}