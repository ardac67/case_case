using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

Env.Load();
var conString = Environment.GetEnvironmentVariable("Conn");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(conString);
});
builder.Services.AddHttpClient(); 
builder.Services.AddHostedService<ListenerThread>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IFilmRepository, FilmRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
