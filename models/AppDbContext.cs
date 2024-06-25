using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Film> Films { get; set; }
    public DbSet<Comments> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);  
        modelBuilder.Entity<Film>()
            .HasIndex(f => f.ReleaseDate)
            .HasDatabaseName("IX_Film_ReleaseDate");
        base.OnModelCreating(modelBuilder);  
        modelBuilder.Entity<Film>()
            .HasIndex(f => f.Id)
            .HasDatabaseName("IX_Film_Id");
    }
}