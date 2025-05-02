using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers;

[Route("tg/posts")]
[Authorize(AuthenticationSchemes = "twa")]
public class TgPostController(PostService postService): ControllerBase
{
    [HttpPost("submit")]
    public async Task Suggest([FromBody] PostSuggestModel model)
    {
        await postService.Create(model.Text, model.Author, null);
    }
}