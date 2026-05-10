namespace CinemaRoulette.Data;

public class DatabaseSettings
{
    public string Server { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string GetConnectionString()
    {
        if (string.IsNullOrEmpty(Server)) throw new InvalidOperationException("Database:Server не задан");
        if (string.IsNullOrEmpty(Port)) throw new InvalidOperationException("Database:Port не задан");
        if (string.IsNullOrEmpty(Name)) throw new InvalidOperationException("Database:Name не задан");
        if (string.IsNullOrEmpty(User)) throw new InvalidOperationException("Database:User не задан");
        if (string.IsNullOrEmpty(Password)) throw new InvalidOperationException("Database:Password не задан");

        return $"Server={Server};Port={Port};Database={Name};User={User};Password={Password};";
    }
}