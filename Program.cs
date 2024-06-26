using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;

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
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
.AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddRateLimiter(optionsOfLimiter => {
    optionsOfLimiter.AddSlidingWindowLimiter("SliderImplementation", param => {
        param.Window = TimeSpan.FromMinutes(1);
        param.PermitLimit = 10;
        param.QueueLimit = 11;
        param.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        param.SegmentsPerWindow = 1;
    }).RejectionStatusCode = 429;
});
builder.Services.AddOutputCache( a =>{
    //global policy
    a.AddBasePolicy(x => x.Expire(TimeSpan.FromMinutes(2)));
    //custom policy
    a.AddPolicy("GetFilms", x => x.Expire(TimeSpan.FromMinutes(1)));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalCatchMiddleware>();
//enable for entire app limit
app.UseRateLimiter();
app.UseOutputCache();
app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
