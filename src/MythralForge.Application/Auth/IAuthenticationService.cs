using MythralForge.Application.Common;

public interface IAuthenticationService
{
    Task<OutcomeResult> RegisterUserAsync(string email, string password);
    Task<Response<string>> LoginAsync(string email, string password);
}
