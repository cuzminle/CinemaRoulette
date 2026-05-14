using CinemaRoulette.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaRoulette.Controllers;

public class CinemaController:Controller
{
    private readonly FilmService _filmService;

    public CinemaController(FilmService filmService)
    {
        _filmService = filmService;
    }

    // ищем кино по id
    public async Task<IActionResult> GetRandomFilm(int genreId, int yearFrom, int yearTo)
    {
        var result = await _filmService.GetRandomFilmAsync(genreId, yearFrom, yearTo);
        return Content(result.ToString(), "application/json");;
    }
}