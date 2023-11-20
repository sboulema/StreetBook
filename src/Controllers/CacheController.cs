using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Threading.Tasks;

namespace StreetBook.Controllers;

[Route("[controller]")]
public class CacheController(IOutputCacheStore outputCacheStore) : Controller
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Clear()
    {
        await outputCacheStore.EvictByTagAsync("streetbook", default);

        return RedirectToAction("Index", "Home");
    }
}
