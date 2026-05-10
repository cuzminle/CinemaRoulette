using System.Data.Common;
using System.Text.Json;
using CinemaRoulette.Data;
using CinemaRoulette.Models;
using CinemaRoulette.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaRoulette.Controllers;

public class CinemaController:Controller
{
    private readonly KinopoiskService _kinopoiskService;
    private readonly AppDbContext _dbContext;

    public CinemaController(KinopoiskService kinopoiskService, AppDbContext dbContext)
    {
        _kinopoiskService = kinopoiskService;
        _dbContext = dbContext;
    }

    // ищем кино по id
    public async Task<IActionResult> Search(string keyword)
    {
        Film cachedFilm = await _dbContext.Films.FirstOrDefaultAsync(film => film.Title == keyword);

        string json;

        if (cachedFilm != null)
        {
            json = cachedFilm.Cinema!;
            return View(json);
        }
        else
        {
            json = await _kinopoiskService.SearchFilmAsync(keyword);
            int id = JsonSerializer.Deserialize<JsonElement>(json).GetProperty("total").GetInt32();
            string title = JsonSerializer.Deserialize<JsonElement>(json).GetProperty("items")[0].GetProperty("nameRu").ToString();
            Film film = new Film
            {
                Id = id,
                Title = title,
                Cinema = json
            };
            _dbContext.Films.Add(film);
            await _dbContext.SaveChangesAsync();
        }
        return Json(json);
    }
}