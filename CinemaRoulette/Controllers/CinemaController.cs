using System.Text.Json;
using CinemaRoulette.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaRoulette.Controllers;

public class CinemaController:Controller
{
    private readonly KinopoiskService _kinopoiskService;

    public CinemaController(KinopoiskService kinopoiskService)
    {
        _kinopoiskService = kinopoiskService;
    }

    public async Task<IActionResult> Search(string keyword)
    {
        JsonElement result = await _kinopoiskService.SearchFilmAsync(keyword);
        ViewBag.Result = result;
        return View();
    }
}