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
    public T? Value => Successful
        ? mValue!
        : throw InvalidResultStateException.ValueException;

    /// <summary>
    /// The protected constructor forces the use of static methods to produce a result
    /// </summary>
    /// <param name="successful">Indicates success of the operation </param>
    /// <param name="error">Indicates an error that occured</param>
    /// <param name="value">The value, if available, or null if errored</param>
    protected internal Result(bool successful, ErrorCollection errors, T? value = default)
        : base(successful, errors)
    {
        mValue = value;
    }

    public void Switch(Action<T> onValue, Action<ErrorCollection> onError)
    {
        if (!Successful)
        {
            onError(Errors);
            return;
        }

        onValue(Value!);
    }

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
    /// <param name="value">the value to return</param>
    public static implicit operator Result<T>(Error error)
    {
        return new(false, new(error));
    }
    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="value">the value to return</param>
    public static implicit operator Result<T>(ErrorCollection errors)
    {
        return new(false, errors);
    }
}
