using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StreetBook.Models.ViewModels;
using Microsoft.AspNetCore.OutputCaching;
using System.Threading.Tasks;

namespace StreetBook.Controllers;

[Route("[controller]")]
public class CacheController : Controller
{
    private readonly IOutputCacheStore _outputCacheStore;

    public CacheController(IOutputCacheStore outputCacheStore)
    {
        _outputCacheStore = outputCacheStore;
    }

    [HttpGet]
    public async Task<IActionResult> Clear()
    {
        await _outputCacheStore.EvictByTagAsync("streetbook", default);

        return Ok();
    }
}
