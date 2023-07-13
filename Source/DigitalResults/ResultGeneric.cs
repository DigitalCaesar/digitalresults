using DigitalCaesar.Results.Exceptions;

namespace DigitalCaesar.Results;

/// <summary>
/// Allows a method to return a result indicating success or failure where the operation may produce an error and or a value
/// </summary>
public class Result<T> : IValueResult<T>
{
    private readonly ErrorCollection mErrors;
    private readonly T? mValue;

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
    {
        // This condition should not happen unless a new factory method is constructed incorrectly
        if (successful && !errors.IsEmpty)
            throw InvalidResultStateException.SuccessException;

        // This condition should not happen unless a new factory method is constructed incorrectly
        if (!successful && errors.IsEmpty)
            throw InvalidResultStateException.FailureException;

        Successful = successful;
        mErrors = errors;
        mValue = value;
    }

    /// <summary>
    /// Switches between Actions dependent on the state of the Result
    /// </summary>
    /// <param name="onSuccess">the Action to execute if the Result is in a success state</param>
    /// <param name="onFailure">the Action to execute if the Result is in a failure state</param>
    public void Switch(Action onSuccess, Action<ErrorCollection> onFailure)
    {
        if (!Successful)
        {
            onFailure(Errors);
            return;
        }

        onSuccess();
    }

    /// <summary>
    /// Switches between Actions dependent on the state of the Result
    /// </summary>
    /// <param name="onSuccess">the Action to execute if the Result is in a success state</param>
    /// <param name="onFailure">the Action to execute if the Result is in a failure state</param>
    public void Switch(Action<T> onSuccess, Action<ErrorCollection> onFailure)
    {
        if (!Successful)
        {
            onFailure(Errors);
            return;
        }

        onSuccess(Value!);
    }


    /// <summary>
    /// Matches the appropriate response based on the state of the Result
    /// </summary>
    /// <typeparam name="R">The type of value to return</typeparam>
    /// <param name="onSuccess">the function to execute if the Result is in a success state</param>
    /// <param name="onFailure">the function to execute if the Result is in a failure state</param>
    /// <returns>Either a Value if successful or an collection of Errors if failed</returns>
    public R Match<R>(
                Func<R> onSuccess,
                Func<ErrorCollection, R> onFailure) =>
            Successful ? onSuccess() : onFailure(Errors);
    /// <summary>
    /// Matches the appropriate response based on the state of the Result
    /// </summary>
    /// <typeparam name="R">The type of value to return</typeparam>
    /// <param name="onSuccess">the function to execute if the Result is in a success state</param>
    /// <param name="onFailure">the function to execute if the Result is in a failure state</param>
    /// <returns>Either a Value if successful or an collection of Errors if failed</returns>
    public R Match<R>(
                Func<T, R> onSuccess,
                Func<ErrorCollection, R> onFailure) =>
            Successful ? onSuccess(Value!) : onFailure(Errors);

    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="value">the value to return</param>
    public static implicit operator Result<T>(T value)
    {
        return new(true, new(), value);
    }
    /// <summary>
    /// Implicit operator converts an Exception to an Error and encapsulates it into a failed result
    /// </summary>
    /// <param name="exception">the Exception to convert</param>
    public static implicit operator Result<T>(Exception exception)
    {
        return new(false, new(new ExceptionError(exception)));
    }
    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="error">the error to convert</param>
    public static implicit operator Result<T>(Error error)
    {
        return new(false, new(error));
    }
    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="errors">the errors to convert</param>
    public static implicit operator Result<T>(ErrorCollection errors)
    {
        return new(false, errors);
    }

    /// <summary>
    /// Implicit operator encapsulates a value into a successful result
    /// </summary>
    /// <param name="success">the value to convert</param>
    public static implicit operator Result<T>(bool success)
    {
        return new(success, new());
    }
    /// <summary>
    /// Implicit operator converts a Result<typeparamref name="T"/> to a Result
    /// </summary>
    /// <param name="result">the result to convert</param>
    public static implicit operator Result(Result<T> result)
    {
        return new(result.Successful, result.mErrors);
    }
}
