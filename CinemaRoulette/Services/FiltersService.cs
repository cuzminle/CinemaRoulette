using CinemaRoulette.Data;
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

    public async Task<string> SaveFiltersDb()
    {
        var json = await _kinopoiskService.GetFiltersAsync();
        return json;
    }
}