﻿using Microsoft.AspNetCore.Mvc;

namespace EnvironmentsExample.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HomeController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [Route("/")]
    public IActionResult Index()
    {
       ViewBag.EnvironmentName = _webHostEnvironment.EnvironmentName;
        return View();
    }

    [Route("/about")]
    public IActionResult About()
    {
        return View();
    }
}