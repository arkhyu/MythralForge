public class OutcomeResult
{
    public bool Success { get; }
    public IEnumerable<string>? Errors { get; }

    public OutcomeResult(bool success, IEnumerable<string>? errors = null)
    {
        Success = success;
        Errors = errors;
    }
}
