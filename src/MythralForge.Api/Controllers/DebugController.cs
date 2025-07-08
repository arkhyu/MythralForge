using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DebugController : ControllerBase
{
    public DebugController()
    {
    }

    [HttpGet("info")]
    public IActionResult GetInfo()
    {
        return Ok("public info");
    }
    [Authorize]
    [HttpGet("info/topsecret")]
    public IActionResult GetTopSecretInfo()
    {
        return Ok("if you don't use a token you should no be able to see this very top secret data");
    }
}