using System.ComponentModel.DataAnnotations;

namespace CinemaRoulette.Models;

public class Genre
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Title { get; set; }
}