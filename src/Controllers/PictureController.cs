using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.OutputCaching;
using StreetBook.Services;

namespace BabyTracker.Controllers;

[Route("[controller]")]
public class PictureController(IPictureService pictureService,
    IHostEnvironment hostEnvironment) : Controller
{
    [OutputCache(PolicyName = "AuthenticatedOutputCache")]
    [Authorize]
    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetPicture(string fileName)
    {
        var picture = await pictureService.GetPicture(hostEnvironment, fileName);

        if (picture == null)
        {
            return NotFound();
        }

        return File(picture, "image/jpg");
    }
}
