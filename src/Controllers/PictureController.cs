using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.OutputCaching;
using StreetBook.Services;

namespace BabyTracker.Controllers;

[Route("[controller]")]
public class PictureController : Controller
{
    private readonly IPictureService _pictureService;
    private readonly IHostEnvironment _hostEnvironment;

    public PictureController(IPictureService pictureService,
        IHostEnvironment hostEnvironment)
    {
        _pictureService = pictureService;
        _hostEnvironment = hostEnvironment;
    }

    [OutputCache(PolicyName = "AuthenticatedOutputCache")]
    [Authorize]
    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetPicture(string fileName)
    {
        var picture = await _pictureService.GetPicture(_hostEnvironment, fileName);

        if (picture == null)
        {
            return NotFound();
        }

        return File(picture, "image/jpg");
    }
}
