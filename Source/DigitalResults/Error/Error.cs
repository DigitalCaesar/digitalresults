namespace DigitalCaesar.Results;

/// <summary>
/// A problem in returning results
/// </summary>
public class Error
{
    /// <summary>
    /// A default empty error
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);
    /// <summary>
    /// A null result error
    /// </summary>
    public static readonly Error NullValue = new(
        "Error.NullValue", 
        "The specified result value is null.");
    /// <summary>
    /// A error in validation
    /// </summary>
    public static readonly Error Validation = new(
        "ValidationError",
        "A validation error occurred.");

    /// <summary>
    /// A unique identifier for the error
    /// </summary>
    public string Code { get; }
    /// <summary>
    /// A message explaining the error
    /// </summary>
    public string Description { get; }
    /// <summary>
    /// The type of underlying issue that trigger the error
    /// </summary>
    public ErrorType ErrorType { get; }


    /// <summary>
    /// Default constructor requires an error code and description
    /// </summary>
    /// <param name="code">the unique identifier of the error</param>
    /// <param name="description">the message explaining the error</param>
    /// <param name="errorType">the type of underlying issue that triggered the error</param>
    public Error(string code, string description, ErrorType errorType = ErrorType.Warning)
    {
        Code = code;
        Description = description;
        ErrorType = errorType;
    }
}
