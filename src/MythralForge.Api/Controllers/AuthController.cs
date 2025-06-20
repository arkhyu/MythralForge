using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;
    private readonly LoginHandler _loginHandler;

    public AuthController(RegisterUserHandler registerUserHandler,
        LoginHandler loginHandler)
    {
        _registerUserHandler = registerUserHandler;
        _loginHandler = loginHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (request.Password != request.ConfirmPassword)
            return BadRequest(new { error = "Passwords do not match." });

        var command = new RegisterCommand(request.Email, request.Password);
        var result = await _registerUserHandler.HandleAsync(command);

        if (!result.Success)
            return BadRequest(new { errors = result.Errors });

        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
       
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _loginHandler.HandleAsync(command);

        if (!result.Success)
            return BadRequest(new { errors = result.Errors });

        return Ok();
    }
}