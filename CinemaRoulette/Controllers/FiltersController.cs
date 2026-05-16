using CinemaRoulette.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CinemaRoulette.Controllers;

public class FiltersController: Controller
{
    private readonly FiltersService _filetersService;

    public FiltersController(FiltersService filtersService)
    {
        _filetersService = filtersService;
    }

    public async Task<IActionResult> GetFilters()
    {
        return Content((await _filetersService.GetFiltersDb()).ToString(), "application/json");
    }
}