
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
    public IActionResult GetFilms()
    {
        Console.WriteLine(userManager.GetUserId(User));
        return Ok(filmRepository.GetAllAsync().Result);
    }
}