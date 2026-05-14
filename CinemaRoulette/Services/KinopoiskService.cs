using System.Text.Json;

namespace CinemaRoulette.Services;

public class KinopoiskService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string BaseUrl = "https://kinopoiskapiunofficial.tech";

    public KinopoiskService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["KinopoiskApiKey"] ?? throw new InvalidOperationException(
            "API ключ Кинопоиска не найден. Добавьте его через dotnet user-secrets set \"KinopoiskApiKey\" \"ваш-ключ\"");
        _httpClient.DefaultRequestHeaders.Add("X-API-KEY", _apiKey);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    /**
     * получить фильм по названию
     */
    public async Task<string> SearchFilmAsync(string keyword)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/api/v2.2/films?keyword={keyword}");
        return await response.Content.ReadAsStringAsync();
    }
    
    /**
     * получить фильм по id 
     */
    public async Task<string> GetFilmByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync(
            $"{BaseUrl}/api/v2.2/films/{id}"
        );
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> CheckApiKeyAsync()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/api/v2.2/api_keys/{_apiKey}");
        return await response.Content.ReadAsStringAsync();
    }

    // Получить список жанров и их ID
    public async Task<string> GetFiltersAsync()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/api/v2.2/films/filters");
        return await response.Content.ReadAsStringAsync();
    }

    // Поиск по фильтрам
    public async Task<string> GetFilmsByFilterAsync(int genreId, int yearFrom, int yearTo, int page = 1)
    {
        var url = $"{BaseUrl}/api/v2.2/films" +
            $"?genres={genreId}" +
            $"&yearFrom={yearFrom}" +
            $"&yearTo={yearTo}" +
            $"&page={page}" +
            $"&order=RATING" +
            $"&type=FILM";
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}