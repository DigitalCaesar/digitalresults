using System.Diagnostics.CodeAnalysis;
using DigitalCaesar.Results.Exceptions;

namespace DigitalCaesar.Results;

/// <summary>
/// Allows a method to return a result indicating success or failure where the operation may produce an error and or a value
/// </summary>
public record struct Result : IResult
{
    private readonly ErrorCollection mErrors;

    /// <summary>
    /// Indicates success of the operation that returned the result
    /// </summary>
    public bool Successful { get; private set; }
    /// <summary>
    /// Indicates an error that occured during the operation
    /// </summary>
    internal ErrorCollection Errors => !Successful
        ? mErrors
        : throw InvalidResultStateException.ValueException;

    /// <summary>
    /// A protected constructor forces the use of static methods to produce a result
    /// </summary>
    /// <param name="successful">Indicates success of the operation </param>
    /// <param name="errors">the errors that occured</param>
    /// <exception cref="InvalidResultStateException">thrown if the state is invalid, for example a successful operation that contains an error or a failed operation that contains no error</exception>
    [ExcludeFromCodeCoverage]
    internal Result(bool successful, ErrorCollection errors)
    {
        // This condition should not happen unless a new factory method is constructed incorrectly
        if (successful && !errors.IsEmpty)
            throw InvalidResultStateException.SuccessException;

        // This condition should not happen unless a new factory method is constructed incorrectly
        if (!successful && errors.IsEmpty)
            throw InvalidResultStateException.FailureException;

        Successful = successful;
        mErrors = errors;
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
    public static Result Failure() => new(false, new(Error.Unknown));
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
    /// Allows a different function to be executed based on the state of the Result
    /// </summary>
    /// <param name="onSuccess">The function to execute if successful</param>
    /// <param name="onError">The function to execute if failed</param>
    public void Switch(Action onSuccess, Action<ErrorCollection> onError)
    {
        if (!Successful)
        {
            onError(Errors);
            return;
        }

        onSuccess();
    }

    /// <summary>
    /// Allows a different value to be returned based on the state of the Result
    /// </summary>
    /// <typeparam name="R">The type of value to return</typeparam>
    /// <param name="success">the function to execute if the Result is in a success state</param>
    /// <param name="failure">the function to execute if the Result is in a failure state</param>
    /// <returns>Either a Value if successful or an collection of Errors if failed</returns>
    public R Match<R>(
                Func<R> success,
                Func<ErrorCollection, R> failure) =>
            Successful ? success() : failure(Errors);

    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="success">the value to return</param>
    public static implicit operator Result(bool success)
    {
        return new(success, new());
    }
}
