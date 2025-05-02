using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Entities;
using WebApplication.Services;

namespace WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController(PostService service, PushService pushService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Post[]>> GetAll()
    {
        var res = await service.GetForUsers();
        foreach (var post in res)
        {
            post.DeviceToken = null;
        }
        return res;
    }

    [Authorize]
    [HttpGet("forAdmin")]
    public async Task<ActionResult<Post[]>> GetForAdmin()
    {
        return await service.GetForAdmin();
    }

    [HttpPost("vote")]
    public async Task<ActionResult> Increment(int id, VoteType voteType)
    {
        await service.IncrementVotes(id, voteType, 1);
        return Ok();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] PostEditModel model)
    {
        await service.Update(id, model);
        return Ok();
    }

    [HttpPost("suggest")]
    public async Task<ActionResult> Suggest(string text, string author, string token)
    {
        await service.Create(text, author, token);
        return Ok();
    }

    [HttpPost("subscribe")]
    public async Task<ActionResult> Subscribe(string token, [FromServices] AppDbContext dbContext)
    {
        dbContext.Set<FirebaseSubscriber>().Add(new FirebaseSubscriber
        {
            DeviceToken = token
        });
        await dbContext.SaveChangesAsync();
        await pushService.SubscribeToPosts(token);
        return Ok();
    }
}