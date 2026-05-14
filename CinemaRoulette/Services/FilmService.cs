using CinemaRoulette.Data;
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

    public async Task<JObject> GetRandomFilmAsync(string keyword)
    {
        Film? cachedFilm = await _dbContext.Films.FirstOrDefaultAsync(film => film.Title == keyword);
        if (cachedFilm != null)
        {
            return JObject.Parse(cachedFilm.Cinema!);
        }
        string json = await _kinopoiskService.SearchFilmAsync(keyword);
        JObject filmArray = JObject.Parse(json);
        JToken firstItem = filmArray["items"]![0];
        Film film = new Film
        {
            Id = (int)firstItem["kinopoiskId"]!,
            Title = firstItem["nameRu"]!.ToString(),
            Cinema = json
        };
        _dbContext.Films.Add(film);
        await _dbContext.SaveChangesAsync();
        return filmArray;
    }

    public async Task<JObject> GetRandomFilmAsync(int genreId, int yearFrom, int yearTo)
    {
        // Получаем первую страницу чтобы узнать totalPages
        var json = await _kinopoiskService.GetFilmsByFilterAsync(genreId, yearFrom, yearTo, page: 1);
        var root = JObject.Parse(json);
        int totalPages = (int)root["totalPages"]!;
        Console.WriteLine(totalPages);
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