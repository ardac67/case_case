using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Film> Films { get; set; }
    public DbSet<Comments> Comments { get; set; }
}