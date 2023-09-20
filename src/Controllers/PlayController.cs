using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StreetBook.Services;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace BabyTracker.Controllers;

[Route("[controller]")]
public class PlayController : Controller
{
    private readonly IStreetBookService _streetBookService;
    private readonly IPlayService _playService;

    public PlayController(
        IStreetBookService streetBookService,
        IPlayService playService)
    {
        _streetBookService = streetBookService;
        _playService = playService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var viewModel = _streetBookService.GetStreetBook();

        viewModel.Persons = viewModel.Persons
            .Where(person => person.HasPicture && person.FirstName != "-")
            .OrderBy(a => Guid.NewGuid())
            .Take(5)
            .ToList();

        viewModel.Highscores = await _playService.GetHighscores();

        return View(viewModel);
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Highscores(string name, int score)
    {
        await _playService.AddHighscore(name, score);

        return Ok();
    }

    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> Highscores()
    {
        var scores = await _playService.GetHighscores();

        return Ok(scores);
    }
}
