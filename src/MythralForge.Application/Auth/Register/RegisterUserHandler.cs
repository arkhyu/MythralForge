public class RegisterUserHandler
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterUserHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<OutcomeResult> HandleAsync(RegisterCommand command)
    {
        return await _authenticationService.RegisterUserAsync(command.Email, command.Password);
    }
    
}
