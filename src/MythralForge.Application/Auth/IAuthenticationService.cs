public interface IAuthenticationService
{
    Task<OutcomeResult> RegisterUserAsync(string email, string password);
    Task<OutcomeResult> LoginAsync(string email, string password);
}
