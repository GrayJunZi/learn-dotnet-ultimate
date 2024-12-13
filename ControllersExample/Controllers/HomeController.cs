using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers;

public class HomeController
{
    [Route("index")]
    public string Index()
    {
        return "Hello from index";
    }
}