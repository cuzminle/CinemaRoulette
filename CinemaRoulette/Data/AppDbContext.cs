using System.Reflection;
using CinemaRoulette.Data.Configurations;
using CinemaRoulette.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaRoulette.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Film>  Films { get; set; }
    public DbSet<Country>  Countries { get; set; }
    public DbSet<Genre>  Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Для design-time (создание миграций)
            var dbSettings = new DatabaseSettings
            {
                Server = "localhost",
                Port = "3306",
                Name = "cinemadb",
                User = "root",
                Password = "rootpassword"
            };
            
            optionsBuilder.UseMySql(
                dbSettings.GetConnectionString(),
                ServerVersion.AutoDetect(dbSettings.GetConnectionString())
            );
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}