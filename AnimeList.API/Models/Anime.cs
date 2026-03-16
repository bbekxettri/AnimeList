using System.ComponentModel.DataAnnotations;

namespace AnimeList.API.Models;

public class Anime
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    [Range(1, 10000)]
    public int Episodes { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }  // Nullable for ongoing anime
    
    public List<string> Genres { get; set; } = new();
    
    [Range(0, 10)]
    public double AverageRating { get; set; }
    
    public string ImageUrl { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
}