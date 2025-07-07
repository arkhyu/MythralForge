using Microsoft.AspNetCore.Identity;

public class AuthenticationService : IAuthenticationService
{
    private readonly TokenRepository _tokenRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AuthenticationService(UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    TokenRepository tokenRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        this._tokenRepository = tokenRepository;
    }

    public async Task<OutcomeResult> RegisterUserAsync(string email, string password)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return new OutcomeResult(false, result.Errors.Select(e => e.Description));

        return new OutcomeResult(true, Enumerable.Empty<string>()); 
    }


    public async Task<(OutcomeResult,string)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return (new OutcomeResult(false, new List<string> { "User not found." }),null);
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (roles != null)
            {
                var token = _tokenRepository.CreateJWTToken(user, roles.ToList());
                return (new OutcomeResult(true, null),token);
            }
        }

        return (new OutcomeResult(false, new List<string> { "Invalid credentials." }),null);
    }
}