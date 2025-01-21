using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers;

public class StoreController : Controller
{
    [Route("store/book/{id}")]
    public IActionResult Book()
    {
        var id = Request.RouteValues["id"];
        
        return Content($"<h1>Book Store {id}</h1>", "text/html");
    }
}