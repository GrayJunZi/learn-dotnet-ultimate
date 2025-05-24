using Microsoft.AspNetCore.Mvc;

namespace CRUDExample.Controllers;

[Route("countries")]
public class CountriesController : Controller
{
    [Route("UploadFromExcel")]
    public async Task<IActionResult> UploadFromExcel(IFormFile file)
    {
        return View();
    }
}