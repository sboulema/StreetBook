using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StreetBook.Services;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Hosting;

namespace BabyTracker.Controllers;

[Route("")]
public class HomeController : Controller
{
    private readonly IStreetBookService _streetBookService;
    private readonly IHostEnvironment _hostEnvironment;

    public HomeController(
        IStreetBookService streetBookService,
        IHostEnvironment hostEnvironment)
    {
        _streetBookService = streetBookService;
        _hostEnvironment = hostEnvironment;
    }

    [OutputCache(PolicyName = "AuthenticatedOutputCache")]
    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var viewModel = _streetBookService.GetStreetBook(_hostEnvironment);

        return View(viewModel);
    }
}
