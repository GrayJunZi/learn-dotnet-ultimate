using Microsoft.AspNetCore.Mvc;

namespace LayoutViewsExample.Controllers;

public class ProductsController : Controller
{
    [Route("/products")]
    public IActionResult Products()
    {
        return View();
    }

    [Route("/search-products")]
    public IActionResult SearchProducts()
    {
        return View();
    }

    [Route("/order-product")]
    public IActionResult OrderProduct()
    {
        return View();
    }
}