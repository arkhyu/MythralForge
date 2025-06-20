public class LoginHandler
{
    private readonly IAuthenticationService _authenticationService;

    public LoginHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<OutcomeResult> HandleAsync(LoginCommand command)
    {
        return await _authenticationService.LoginAsync(command.Email, command.Password);
    }
}
