using CinemaRoulette.Data;
using CinemaRoulette.DTOs;
using CinemaRoulette.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CinemaRoulette.Services;

public class FilmService
{
    private readonly KinopoiskService _kinopoiskService;
    private readonly AppDbContext _dbContext;

    public FilmService(KinopoiskService kinopoiskService, AppDbContext dbContext)
    {
        _kinopoiskService = kinopoiskService;
        _dbContext = dbContext;
    }

    public async Task<JObject> GetRandomFilmAsync(FilterQuery query)
    {
        // Получаем первую страницу чтобы узнать totalPages
        var json = await _kinopoiskService.GetFilmsByFilterAsync(query);
        var root = JObject.Parse(json);
        int totalPages = (int)root["totalPages"]!;
        // Берём случайную страницу
        var random = new Random();
        int randomPage = random.Next(1, Math.Min(totalPages + 1, 10)); // не больше 10 страниц
        // Получаем случайную страницу
        json = await _kinopoiskService.GetFilmsByFilterAsync(genreId, yearFrom, yearTo, randomPage);
        root = JObject.Parse(json);
        var items = root["items"] as JArray;
        // Берём случайный фильм со страницы
        int randomIndex = random.Next(0, items!.Count);
        return (JObject)items[randomIndex]!;
    }
}