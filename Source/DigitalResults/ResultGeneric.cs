using DigitalCaesar.Results.Exceptions;

namespace DigitalCaesar.Results;

/// <summary>
/// Allows a method to return a result indicating success or failure where the operation may produce an error and or a value
/// </summary>
public class Result<T> : Result
{
    private readonly T? mValue;

    /// <summary>
    /// The value of a successful result
    /// </summary>
    protected T? Value => Successful
        ? mValue!
        : throw InvalidResultStateException.ValueException;

    /// <summary>
    /// The protected constructor forces the use of static methods to produce a result
    /// </summary>
    /// <param name="successful">Indicates success of the operation </param>
    /// <param name="errors">Indicates the errors that occured</param>
    /// <param name="value">The value, if available, or null if errored</param>
    protected internal Result(bool successful, ErrorCollection errors, T? value = default)
        : base(successful, errors)
    {
        mValue = value;
    }

    /// <summary>
    /// Allows a different function to be executed based on the state of the Result
    /// </summary>
    /// <param name="onValue">The function to execute if successful</param>
    /// <param name="onError">The function to execute if failed</param>
    public void Switch(Action<T> onValue, Action<ErrorCollection> onError)
    {
        if (!Successful)
        {
            onError(Errors);
            return;
        }

        onValue(Value!);
    }

    /// <summary>
    /// Allows a different value to be returned based on the state of the Result
    /// </summary>
    /// <typeparam name="R">The type of value to return</typeparam>
    /// <param name="success">the function to execute if the Result is in a success state</param>
    /// <param name="failure">the function to execute if the Result is in a failure state</param>
    /// <returns>Either a Value if successful or an collection of Errors if failed</returns>
    public R Match<R>(
                Func<T, R> success,
                Func<ErrorCollection, R> failure) =>
            Successful ? success(Value!) : failure(Errors);

    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="value">the value to return</param>
    public static implicit operator Result<T>(T value)
    {
        return new(true, new(), value);
    }
    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="error">the error to return</param>
    public static implicit operator Result<T>(Exception exception)
    {
        return new(false, new(new ExceptionError(exception)));
    }
    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="error">the error to return</param>
    public static implicit operator Result<T>(Error error)
    {
        return new(false, new(error));
    }
    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="errors">the errors to return</param>
    public static implicit operator Result<T>(ErrorCollection errors)
    {
        return new(false, errors);
    }

    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="success">the value to return</param>
    public static implicit operator Result<T>(bool success)
    {
        return new(success, new());
    }
}
