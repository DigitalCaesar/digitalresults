namespace DigitalCaesar.Results;

/// <summary>
/// Error from Exception
/// </summary>
public class ExceptionError : Error<Exception>
{
    /// <summary>
    /// Constructor explicitly sets each error value separate from the Exception data
    /// </summary>
    /// <param name="code">the unique identifier of the error</param>
    /// <param name="description">the message explaining the error</param>
    /// <param name="errorData">the exception that triggered the error</param>
    public ExceptionError(string code, string description, Exception errorData)
        : base(code, description, errorData, ErrorType.Exception) { }

    /// <summary>
    /// Constructor requires an exception
    /// </summary>
    /// <param name="errorData">the exception that triggered the error</param>
    public ExceptionError(Exception errorData) 
        : base(errorData.GetType().Name, errorData.Message, errorData, ErrorType.Exception) { }
}
