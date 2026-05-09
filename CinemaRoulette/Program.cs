using CinemaRoulette.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<KinopoiskService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
    name: "Movie",
    pattern: "Movie/{id?}",
    defaults: new { controller = "Blog", action = "Post" }
);
app.MapGet("/test-api", async (KinopoiskService kinopoisk) =>
{
    var result = await kinopoisk.SearchFilmAsync("Матрица");
    return result;
});

app.Run();
