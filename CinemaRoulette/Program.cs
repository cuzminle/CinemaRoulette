using CinemaRoulette.Data;
using CinemaRoulette.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Привязываем секцию Database к классу
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));

// достаем данные для подключения к БД
var dbSettings = builder.Configuration.GetSection("Database").Get<DatabaseSettings>()
                 ?? throw new InvalidOperationException("Настройки БД не найдены");

var connectionString = dbSettings.GetConnectionString();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0))));
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<KinopoiskService>();
builder.Services.AddScoped<FilmService>();
var app = builder.Build();

// Применяем миграции
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "GetRandomFilm",
    pattern: "Cinema/Random/{genreId}/{yearFrom}/{yearTo}",
    defaults: new { controller = "Cinema", action = "GetRandomFilm" }
);

app.Run();