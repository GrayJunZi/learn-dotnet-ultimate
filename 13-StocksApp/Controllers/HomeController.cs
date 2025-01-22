using Microsoft.AspNetCore.Mvc;
using StocksApp.ServiceContracts;

namespace StocksApp.Controllers;

public class HomeController : Controller
{
    private readonly IPostService _postService;

    public HomeController(IPostService postService)
    {
        _postService = postService;
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        return View(await _postService.GetPosts());
    }
}