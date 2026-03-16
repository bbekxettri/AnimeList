using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeList.API.Models;
using AnimeList.API.Data;

namespace AnimeList.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly AppDbContext _context;

    public AnimeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var animes = await _context.Animes.ToListAsync();
        return Ok(animes);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var anime = await _context.Animes.FindAsync(id);
        if (anime == null)
            return NotFound($"Anime with ID {id} not found");
    
        return Ok(anime);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Anime anime)
    {
        if (anime == null)
            return BadRequest("Anime cannot be null");
        
        if (string.IsNullOrWhiteSpace(anime.Title))
            return BadRequest("Title is required");

        anime.CreatedAt = DateTime.UtcNow;
        _context.Animes.Add(anime);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = anime.Id }, anime);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Anime updatedAnime)
    {
        var existingAnime = await _context.Animes.FindAsync(id);
        if (existingAnime == null)
            return NotFound($"Anime with ID {id} not found");
        
        // Update properties
        existingAnime.Title = updatedAnime.Title;
        existingAnime.Description = updatedAnime.Description;
        existingAnime.Episodes = updatedAnime.Episodes;
        existingAnime.ReleaseDate = updatedAnime.ReleaseDate;
        existingAnime.EndDate = updatedAnime.EndDate;
        existingAnime.Genres = updatedAnime.Genres;
        existingAnime.AverageRating = updatedAnime.AverageRating;
        existingAnime.ImageUrl = updatedAnime.ImageUrl;
        existingAnime.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return Ok(existingAnime);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var anime = await _context.Animes.FindAsync(id);
        if (anime == null)
            return NotFound($"Anime with ID {id} not found");
        
        _context.Animes.Remove(anime);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}