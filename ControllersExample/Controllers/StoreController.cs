using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers;

public class StoreController : Controller
{
    [Route("store/book")]
    public IActionResult Book()
    {
        return Content("<h1>Store book!</h1>", "text/html");
    }
}