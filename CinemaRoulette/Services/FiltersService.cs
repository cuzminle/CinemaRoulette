using CinemaRoulette.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CinemaRoulette.Services;

public class FiltersService
{
    private readonly KinopoiskService _kinopoiskService;
    private readonly AppDbContext _dbContext;

    public FiltersService(AppDbContext dbContext, KinopoiskService kinopoiskService)
    {
        _kinopoiskService = kinopoiskService;
        _dbContext = dbContext;
    }

    public async Task<JObject> GetFiltersDb()
    {
        var genres = await _dbContext.Genres.ToListAsync();
        var countries = await _dbContext.Countries.ToListAsync();
        return new JObject
        {
            ["genres"] = JArray.FromObject(genres),
            ["countries"] = JArray.FromObject(countries),
        };
    }
}