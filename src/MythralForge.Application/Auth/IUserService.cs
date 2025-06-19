public interface IUserService
{
    Task<(bool Success, IEnumerable<string> Errors)> RegisterUserAsync(string email, string password);
}
