
using Microsoft.EntityFrameworkCore;

public class FilmRepository : IFilmRepository
{
    private readonly AppDbContext db;
    public FilmRepository(AppDbContext db)
    {
        this.db = db;
    }

    public Task<List<Film>> GetAllAsync()
    {
        return db.Films.ToListAsync();
    }

    public async Task SaveAsync(Comments comment)
    {
        db.Comments.Add(comment);
        await db.SaveChangesAsync();
    }
}