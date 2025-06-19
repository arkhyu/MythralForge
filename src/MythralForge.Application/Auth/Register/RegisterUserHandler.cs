using Microsoft.AspNetCore.Identity;

public class RegisterUserHandler
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterUserHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterUserResult> HandleAsync(RegisterCommand command)
    {
        var user = new IdentityUser
        {
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            return new RegisterUserResult(false, result.Errors.Select(e => e.Description));
        }

        return new RegisterUserResult(true);
    }
}
