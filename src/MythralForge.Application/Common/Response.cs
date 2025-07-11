namespace MythralForge.Application.Common;

/// <summary>
/// Generic response wrapper for operations that return data
/// </summary>
/// <typeparam name="T">The type of data being returned</typeparam>
public class Response<T>
{
    /// <summary>
    /// Gets whether the operation was successful
    /// </summary>
    public bool IsSuccess => Outcome.Success;
    
    /// <summary>
    /// Gets the outcome result containing success status and any errors
    /// </summary>
    public OutcomeResult Outcome { get; }
    
    /// <summary>
    /// Gets the data if the operation was successful, null otherwise
    /// </summary>
    public T? Data { get; }
    
    /// <summary>
    /// Gets any error messages if the operation failed
    /// </summary>
    public IEnumerable<string>? Errors => Outcome.Errors;

    /// <summary>
    /// Initializes a new instance of the Response class
    /// </summary>
    /// <param name="outcome">The outcome result of the operation</param>
    /// <param name="data">The data if successful</param>
    public Response(OutcomeResult outcome, T? data = default)
    {
        Outcome = outcome ?? throw new ArgumentNullException(nameof(outcome));
        Data = data;
    }

    /// <summary>
    /// Creates a successful response
    /// </summary>
    /// <param name="data">The data to return</param>
    /// <returns>A successful response</returns>
    public static Response<T> Success(T data)
    {
        return new Response<T>(new OutcomeResult(true), data);
    }

    /// <summary>
    /// Creates a failed response
    /// </summary>
    /// <param name="errors">The error messages</param>
    /// <returns>A failed response</returns>
    public static Response<T> Failure(IEnumerable<string> errors)
    {
        return new Response<T>(new OutcomeResult(false, errors));
    }

    /// <summary>
    /// Creates a failed response with a single error message
    /// </summary>
    /// <param name="error">The error message</param>
    /// <returns>A failed response</returns>
    public static Response<T> Failure(string error)
    {
        return new Response<T>(new OutcomeResult(false, new[] { error }));
    }
} 