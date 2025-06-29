﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CRUDExample.Controllers;

public class HomeController : Controller
{
    public IActionResult Error()
    {
        var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
        {
            ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
        }

        return View();
    }
}