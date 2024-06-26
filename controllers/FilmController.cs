
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


[Route("/films")]
[Authorize]
[ApiController]
public class FilmController: ControllerBase
{
    private readonly IFilmRepository filmRepository;
    private readonly UserManager<IdentityUser> userManager;
    public FilmController(IFilmRepository filmRepository,UserManager<IdentityUser> userManager)
    {
        this.filmRepository = filmRepository;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetFilms([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var films = await filmRepository.GetFilmsLimitedAsync(pageNumber, pageSize);
        var filmDtos = films.Select(f => new FilmDto
        {
            Id = f.Id,
            Adult = f.Adult,
            BackdropPath = f.BackdropPath,
            GenreIds = f.GenreIds,
            OriginalLanguage = f.OriginalLanguage,
            OriginalTitle = f.OriginalTitle,
            Overview = f.Overview,
            Popularity = f.Popularity,
            PosterPath = f.PosterPath,
            ReleaseDate = f.ReleaseDate,
            Title = f.Title,
            Video = f.Video,
            VoteAverage = f.VoteAverage,
            VoteCount = f.VoteCount,
            Comments = f.comments.Select(c => new CommentDto
            {
                Comment = c.Comment,
                FilmId = c.FilmId,
                Point = c.Point
            }).ToList()
        }).ToList();

        return Ok(filmDtos);
    }


    [HttpGet("search")]
    public async Task<IActionResult> getFilmsByName([FromQuery] string filmTitle = ""){
        var films = await filmRepository.GetFilmsByNameAsync(filmTitle);
        var filmDtos = films.Select(f => new FilmDto
        {
            Id = f.Id,
            Adult = f.Adult,
            BackdropPath = f.BackdropPath,
            GenreIds = f.GenreIds,
            OriginalLanguage = f.OriginalLanguage,
            OriginalTitle = f.OriginalTitle,
            Overview = f.Overview,
            Popularity = f.Popularity,
            PosterPath = f.PosterPath,
            ReleaseDate = f.ReleaseDate,
            Title = f.Title,
            Video = f.Video,
            VoteAverage = f.VoteAverage,
            VoteCount = f.VoteCount,
            Comments = f.comments.Select(c => new CommentDto
            {
                Comment = c.Comment,
                FilmId = c.FilmId,
                Point = c.Point
            }).ToList()
        }).ToList();
        if(filmDtos.Count == 0)
        {
            var response = new { message = "No film found with the given title" };
            return BadRequest(response);
        }
        else{
            return Ok(filmDtos);
        }
    }

    [HttpPost("sendSuggestion")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.GetUserAsync(User);
            var film = await filmRepository.GetFilmByIdAsync(request.film_id);
            if(film == null)
            {
                return BadRequest("Invalid film id");
            }
            var mail_body = new { Sender = user.Email, Suggested_Movie = film.Title, Description = film.Overview};
            await filmRepository.SendEmailAsync(request.To, mail_body.Suggested_Movie+" suggested by "+mail_body.Sender, mail_body.Description);
            return Ok();
        }
        return BadRequest(ModelState);
    }

}