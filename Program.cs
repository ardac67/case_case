using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
