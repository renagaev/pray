using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public ActionResult CheckToken()
    {
        return Ok();
    }
}