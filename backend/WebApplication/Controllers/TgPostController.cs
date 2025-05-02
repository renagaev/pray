using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;

[Route("tg/posts")]
[Authorize(AuthenticationSchemes = "twa")]
public class TgPostController: ControllerBase
{
    [HttpPost("submit")]
    public async Task<ActionResult> Suggest(string text, string author)
    {
        return Ok();
    }
}