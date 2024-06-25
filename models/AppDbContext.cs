using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Film> Films { get; set; }
    public DbSet<Comments> Comments { get; set; }
}