namespace DigitalCaesar.Results.Exceptions;


/// <summary>
/// An exception where the requested result state is invalid.  For example, the result cannot be successful with errors or failed without errors.
/// </summary>
public class InvalidResultStateException : ResultsException
{
    private InvalidResultStateException(string message) : base(message) { }

    /// <summary>
    /// Thrown when a successful result contains errors
    /// </summary>
    public static InvalidResultStateException SuccessException
        => new("A result cannot be successful with errors");
    /// <summary>
    /// Thrown when a failure result does not contain errors
    /// </summary>
    public static InvalidResultStateException FailureException
        => new("A result cannot be a failure without errors");

    /// <summary>
    /// Thrown when attempting to access the value of a failure result
    /// </summary>
    public static InvalidResultStateException ValueException
        => new("A failure result cannot contain a value");
}