using CinemaRoulette.Services;

namespace CinemaRoulette.DTOs;

public class FilterQuery
{
    public int[]? GenreId { get; set; }
    public int[]? CountryId { get; set; }
    public int? Duration { get; set; }
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
}