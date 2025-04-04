﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StreetBook.Services;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace StreetBook.Controllers;

[Route("[controller]")]
public class PlayController(
    IStreetBookService streetBookService,
    IPlayService playService) : Controller
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var (viewModel, errorMessage) = streetBookService.GetStreetBook();

        if (!string.IsNullOrEmpty(errorMessage))
        {
            TempData["notificationMessage"] = errorMessage;
            TempData["notificationType"] = "danger";
        }

        viewModel.Persons = [.. viewModel.Persons
            .Where(person => person.HasPicture && person.FirstName != "-")
            .OrderBy(a => Guid.NewGuid())
            .Take(5)];

        viewModel.HighScores = await playService.GetHighScores();

        return View(viewModel);
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> HighScores(string name, int score)
    {
        await playService.AddHighScore(name, score);

        return Ok();
    }

    [HttpGet("[action]")]
    [Authorize]
    public async Task<IActionResult> HighScores()
    {
        var scores = await playService.GetHighScores();

        return Ok(scores);
    }
}
