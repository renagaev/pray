using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;

[Route("posts/tg")]
[Authorize(AuthenticationSchemes = "tma")]
public class TgPostController: ControllerBase
{
    [HttpPost("suggest")]
    public async Task<ActionResult> Suggest(string text, string author)
    {
        return Ok();
    }
}