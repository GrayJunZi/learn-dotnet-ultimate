using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CRUDExample.Controllers;

[Route("countries")]
public class CountriesController : Controller
{
    private readonly ICountriesService _countriesService;

    public CountriesController(ICountriesService countriesService)
    {
        _countriesService = countriesService;
    }

    [HttpGet]
    [Route("UploadFromExcel")]
    public async Task<IActionResult> UploadFromExcel()
    {
        return View();
    }

    [HttpPost]
    [Route("UploadFromExcel")]
    public async Task<IActionResult> UploadFromExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ViewBag.ErrorMessage = "Please select an xlsx file";
            return View();
        }

        if (Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.ErrorMessage = "Unsupported file. 'xlsx' file is expected";
            return View();
        }

        var countriesCountInserted = await _countriesService.UploadCountriesFromExcelFile(file);

            ViewBag.Message = $"{countriesCountInserted} Countries Uploaded";
        return View();
    }
}