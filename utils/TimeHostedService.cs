using Newtonsoft.Json;
using DotNetEnv;

public class ListenerThread : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IServiceScopeFactory _scopeFactory;

    public ListenerThread(IHttpClientFactory httpClientFactory, IServiceScopeFactory scopeFactory)
    {
        _httpClientFactory = httpClientFactory;
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12));
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                Env.Load();
                var response = await httpClient.GetAsync(Environment.GetEnvironmentVariable("ROUTE") +"?api_key=" + Environment.GetEnvironmentVariable("APIKEY") + "&language=en-US&page=1");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var movieResponse = JsonConvert.DeserializeObject<FilmGetDto>(data);
                if(movieResponse != null)
                {
                    List<Film> films = movieResponse.Results.Select(dto => new Film
                    {
                        Id = Guid.NewGuid(),
                        Adult = dto.Adult,
                        BackdropPath = dto.backdrop_path,
                        GenreIds = dto.genre_ids,
                        OriginalLanguage = dto.original_language,
                        OriginalTitle = dto.original_title,
                        Overview = dto.overview,
                        Popularity = dto.popularity,
                        PosterPath = dto.poster_path,
                        ReleaseDate = dto.release_date,
                        Title = dto.title,
                        Video = dto.Video,
                        VoteAverage = dto.vote_average,
                        VoteCount = dto.vote_count
                    }).ToList();
                    db.AddRange(films);
                    await db.SaveChangesAsync(); 
                }
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}