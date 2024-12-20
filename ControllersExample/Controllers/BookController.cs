using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ControllersExample.Controllers;

public class BookController : Controller
{
    [Route("book")]
    public IActionResult Index()
    {
        if (!Request.Query.ContainsKey("bookid"))
        {
            Response.StatusCode = 400;  
            return Content("Book ID is required"); 
        }

        if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
        {
            Response.StatusCode = 400;
            return Content("Book ID can't be null or empty");
        }
        
        var bookId = Convert.ToInt16(Request.Query["bookid"]);
        if (bookId<=0)
        {
            Response.StatusCode = 400;
            return Content("Book ID can't be less than or equal to zero");
        }

        if (bookId > 1000)
        {
            Response.StatusCode = 400;
            return Content("Book ID can't be greater than 1000");
        }

        if (!Convert.ToBoolean(Request.Query["isloggedin"]))
        {
            Response.StatusCode = 401;
            return Content("User must be authenticated");
        }
        
        return File("/sample.txt", "text/plain");
    }
}