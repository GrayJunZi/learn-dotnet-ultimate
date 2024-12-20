using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ControllersExample.Controllers;

public class BookController : Controller
{
    [Route("store")]
    public IActionResult Store()
    {
        // 301 - Permanently
        // return RedirectToActionPermanent("Book","Store", new {});
        // return new RedirectToActionResult("Book", "Store", new {}, true);

        // 302 - Found
        // return RedirectToAction("Book", "Store", new { Id = 1001 });
        
        // return LocalRedirect($"store/book/{1123}");
        // return LocalRedirectPermanent($"store/book/{2223}");
        
        // return Redirect($"store/book/{3333}");
        return RedirectPermanent($"store/book/{4434}");
    }

    [Route("book")]
    public IActionResult Index()
    {
        if (!Request.Query.ContainsKey("bookid"))
        {
            return BadRequest("Book ID is not supplied");
        }

        if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
        {
            return BadRequest("Book ID can't be null or empty");
        }

        var bookId = Convert.ToInt16(Request.Query["bookid"]);
        if (bookId <= 0)
        {
            return BadRequest("Book ID can't be less than or equal to zero");
        }

        if (bookId > 1000)
        {
            return NotFound("Book ID can't be greater than 1000");
        }

        if (!Convert.ToBoolean(Request.Query["isloggedin"]))
        {
            return Unauthorized("User must be authenticated");
        }

        return File("/sample.txt", "text/plain");
    }
}