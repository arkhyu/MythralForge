public interface IAuthenticationService
{
    Task<OutcomeResult> RegisterUserAsync(string email, string password);
    Task<(OutcomeResult,string)> LoginAsync(string email, string password);
}
