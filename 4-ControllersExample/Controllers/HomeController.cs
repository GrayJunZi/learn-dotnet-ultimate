using ControllersExample.Models;
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

    [Route("person")]
    public JsonResult Person()
    {
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "James",
            Age = 25
        };
        return Json(person);
    }

    [Route("file-download-virtual")]
    public FileResult FileDownloadVirtual()
    {
        return new VirtualFileResult("/sample.txt", "text/plain");
    }
    
    [Route("file-download-physical")]
    public FileResult FileDownloadPhysical()
    {
        return new PhysicalFileResult("d:/sample.txt", "text/plain");
    }
    
    [Route("file-download-bytes")]
    public FileResult FileDownloadBytes()
    {
        var bytes = System.IO.File.ReadAllBytes("sample.txt");
        return new FileContentResult(bytes, "text/plain");
    }

    [Route("file-download")]
    public FileResult FileDownload()
    {
        return File("sample.txt", "text/plain");
    }
}