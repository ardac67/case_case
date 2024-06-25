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

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12));
        await runOnce();
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12));
        //return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                Env.Load();
                List<Film> films = new List<Film>();
                string maxPageStr = Environment.GetEnvironmentVariable("MAX_PAGE");
                int maxPage = 1;
                if (!int.TryParse(maxPageStr, out maxPage))
                {
                    maxPage = 5;
                }

                for(int i =0 ; i < maxPage; i++){
                    var response = await httpClient.GetAsync(Environment.GetEnvironmentVariable("ROUTE_UPCOMING") +"?api_key=" + Environment.GetEnvironmentVariable("APIKEY") + $"&language=en-US&page={i+1}");
                    //Console.WriteLine(Environment.GetEnvironmentVariable("ROUTE") +"?api_key=" + Environment.GetEnvironmentVariable("APIKEY") + $"&language=en-US&page=10");
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    var movieResponse = JsonConvert.DeserializeObject<FilmGetDto>(data);
                    if(movieResponse != null)
                    {
                        films.AddRange(movieResponse.Results.Select(dto => new Film
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
                        }).ToList());
                    }
                }
                /* foreach(var film in films)
                {
                    Console.WriteLine(film.BackdropPath);
                } */

                /* Dictionary<string, Film> dic=db.Films.ToList().ToDictionary(f => f.Title);
                foreach(var film in films)
                {
                    if(dic.ContainsKey(film.Title))
                    {
                        db.Films.Update(film);
                    }
                    else{
                        db.Films.Add(film);
                    }
                } */
                db.AddRange(films);
                await db.SaveChangesAsync();
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
    public async Task runOnce(){
        using (var scope = _scopeFactory.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                Env.Load();
                List<Film> films = new List<Film>();
                string maxPageStr = Environment.GetEnvironmentVariable("MAX_PAGE");
                int maxPage = 1;
                if (!int.TryParse(maxPageStr, out maxPage))
                {
                    maxPage = 5;
                }

                for(int i =0 ; i < maxPage; i++){
                    var response = await httpClient.GetAsync(Environment.GetEnvironmentVariable("ROUTE") +"?api_key=" + Environment.GetEnvironmentVariable("APIKEY") + $"&language=en-US&page={i+1}");
                    //Console.WriteLine(Environment.GetEnvironmentVariable("ROUTE") +"?api_key=" + Environment.GetEnvironmentVariable("APIKEY") + $"&language=en-US&page=10");
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    var movieResponse = JsonConvert.DeserializeObject<FilmGetDto>(data);
                    if(movieResponse != null)
                    {
                        films.AddRange(movieResponse.Results.Select(dto => new Film
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
                        }).ToList());
                    }
                }
                /* foreach(var film in films)
                {
                    Console.WriteLine(film.BackdropPath);
                } */

                db.AddRange(films);
                await db.SaveChangesAsync(); 
            }
 
        }
    } 
}