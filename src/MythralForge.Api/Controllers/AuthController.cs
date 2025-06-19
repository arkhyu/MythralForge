using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;

    public AuthController(RegisterUserHandler registerUserHandler)
    {
        _registerUserHandler = registerUserHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (request.Password != request.ConfirmPassword)
            return BadRequest(new { error = "Passwords do not match." });

        var command = new RegisterCommand(request.Email, request.Password);
        var result = await _registerUserHandler.HandleAsync(command);

        if (!result.Success)
            return BadRequest(new { errors = result.Errors });

        return Ok(new { message = "User registered successfully." });
    }
}