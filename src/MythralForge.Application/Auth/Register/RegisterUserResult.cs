public class RegisterUserResult
{
    public bool Success { get; }
    public IEnumerable<string>? Errors { get; }

    public RegisterUserResult(bool success, IEnumerable<string>? errors = null)
    {
        Success = success;
        Errors = errors;
    }
}
