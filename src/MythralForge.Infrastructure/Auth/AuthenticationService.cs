using Microsoft.AspNetCore.Identity;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AuthenticationService(UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<OutcomeResult> RegisterUserAsync(string email, string password)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return new OutcomeResult(false, result.Errors.Select(e => e.Description));

        return new OutcomeResult(true, Enumerable.Empty<string>()); 
    }


    public async Task<OutcomeResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new OutcomeResult(false, new List<string> { "User not found." });
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (result.Succeeded)
        {
            return new OutcomeResult(true, null);
        }

        return new OutcomeResult(false, new List<string> { "Invalid credentials." });
    }
}