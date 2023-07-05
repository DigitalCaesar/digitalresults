using DigitalCaesar.Results.Exceptions;

namespace DigitalCaesar.Results;

/// <summary>
/// Allows a method to return a result indicating success or failure where the operation may produce an error and or a value
/// </summary>
public class Result 
{
    /// <summary>
    /// Indicates success of the operation that returned the result
    /// </summary>
    public bool Successful { get; private set; }
    /// <summary>
    /// Indicates an error that occured during the operation
    /// </summary>
    public ErrorCollection Errors { get; private set; }

    /// <summary>
    /// A protected constructor forces the use of static methods to produce a result
    /// </summary>
    /// <param name="successful">Indicates success of the operation </param>
    /// <param name="errors">the errors that occured</param>
    /// <exception cref="InvalidResultStateException">thrown if the state is invalid, for example a successful operation that contains an error or a failed operation that contains no error</exception>
    protected internal Result(bool successful, ErrorCollection errors)
    {
        if (successful && !errors.IsEmpty)
            throw InvalidResultStateException.SuccessException;

        if (!successful && errors.IsEmpty)
            throw InvalidResultStateException.FailureException;

        Successful = successful;
        Errors = errors;
    }

    /// <summary>
    /// Creates a successful result without a value to return
    /// </summary>
    /// <returns>A successful result</returns>
    public static Result Success() => new(true, new());
    /// <summary>
    /// Creates a successful result with a value to return
    /// </summary>
    /// <typeparam name="T">the value type of the result to return</typeparam>
    /// <param name="value">the value of the result to return</param>
    /// <returns>A successful result</returns>
    public static Result<T> Success<T>(T value) => new(true, new(), value);
    /// <summary>
    /// Creates a failure result without a value or error to return
    /// </summary>
    /// <returns>A failure result</returns>
    public static Result Failure() => new(false, new(Error.None));
    /// <summary>
    /// Creates a failure result without a value to return
    /// </summary>
    /// <param name="error">The error that occurred</param>
    /// <returns>A failure result</returns>
    public static Result Failure(Error error) => new(false, new(error));
    /// <summary>
    /// Creates a failure result without a value to return
    /// </summary>
    /// <param name="errors">The errors that occurred</param>
    /// <returns>A failure result</returns>
    public static Result Failure(ErrorCollection errors) => new(false, errors);
    /// <summary>
    /// Creates a failure result with a value to return
    /// </summary>
    /// <typeparam name="T">the value type of the result to return</typeparam>
    /// <param name="error">The error that occurred</param>
    /// <returns>A failure result</returns>
    public static Result<T> Failure<T>(Error error) => new(false, new(error));
    /// <summary>
    /// Creates a failure result with a value to return
    /// </summary>
    /// <typeparam name="T">the value type of the result to return</typeparam>
    /// <param name="errors">The errors that occurred</param>
    /// <returns>A failure result</returns>
    public static Result<T> Failure<T>(ErrorCollection errors) => new(false, errors);


    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="success">the value to return</param>
    public static implicit operator Result(bool success)
    {
        return new(success, new());
    }
}
