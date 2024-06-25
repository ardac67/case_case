
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


[Route("/comment")]
[Authorize]
[ApiController]
public class CommentController: ControllerBase
{
    private readonly IFilmRepository filmRepository;
    private readonly UserManager<IdentityUser> userManager;
    public CommentController(IFilmRepository filmRepository,UserManager<IdentityUser> userManager)
    {
        this.filmRepository = filmRepository;
        this.userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> PostComment([FromBody] CommentDto cDto)
    {
        var comment = cDto.ParseComment();
        var user_id = userManager.GetUserId(User);
        if (Guid.TryParse(user_id, out Guid userId))
        {
            comment.UserId = userId;
        }    
        else {
            return BadRequest("Invalid user id");
        }  
        await filmRepository.SaveAsync(comment);  
        return Ok(comment);
    }
}