using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StreetBook.Services;
using Microsoft.AspNetCore.OutputCaching;

namespace BabyTracker.Controllers;

[Route("")]
public class HomeController : Controller
{
    private readonly IStreetBookService _streetBookService;

    public HomeController(IStreetBookService streetBookService)
    {
        _streetBookService = streetBookService;
    }

    [OutputCache(PolicyName = "AuthenticatedOutputCache")]
    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var viewModel = _streetBookService.GetStreetBook();

        ViewBag.GutterWidth = 0;

        return View(viewModel);
    }
}
