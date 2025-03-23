using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StreetBook.Services;
using Microsoft.AspNetCore.OutputCaching;

namespace StreetBook.Controllers;

[Route("")]
public class HomeController(IStreetBookService streetBookService) : Controller
{
    [OutputCache(PolicyName = "AuthenticatedOutputCache")]
    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        var (viewModel, errorMessage) = streetBookService.GetStreetBook();

        if (!string.IsNullOrEmpty(errorMessage))
        {
            TempData["notificationMessage"] = errorMessage;
            TempData["notificationType"] = "danger";
        }

        ViewBag.GutterWidth = 0;

        return View(viewModel);
    }
}
