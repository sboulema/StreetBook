using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StreetBook.Services;
using Microsoft.AspNetCore.OutputCaching;

namespace BabyTracker.Controllers;

[Route("")]
public class HomeController(IStreetBookService streetBookService) : Controller
{
    [OutputCache(PolicyName = "AuthenticatedOutputCache")]
    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var viewModel = streetBookService.GetStreetBook();

        ViewBag.GutterWidth = 0;

        return View(viewModel);
    }
}
