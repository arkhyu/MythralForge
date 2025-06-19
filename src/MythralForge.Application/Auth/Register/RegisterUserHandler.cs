public class RegisterUserHandler
{
    private readonly IUserService _userService;

    public RegisterUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<RegisterUserResult> HandleAsync(RegisterCommand command)
    {
        var (success, errors) = await _userService.RegisterUserAsync(command.Email, command.Password);

        return new RegisterUserResult(success, errors);
    }
}
