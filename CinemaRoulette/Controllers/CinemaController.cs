using CinemaRoulette.Services;
using Microsoft.AspNetCore.Mvc;
using CinemaRoulette.DTOs;
namespace CinemaRoulette.Controllers;

public class CinemaController:Controller
{
    private readonly FilmService _filmService;

    public CinemaController(FilmService filmService)
    {
        _filmService = filmService;
    }

    // ищем кино по id
    public async Task<IActionResult> GetRandomFilm([FromQuery] FilterQuery query)
    {
        var result = await _filmService.GetRandomFilmAsync(query);
        return Content(result.ToString(), "application/json");
    }
}