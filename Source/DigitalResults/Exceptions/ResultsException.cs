namespace DigitalCaesar.Results.Exceptions;

/// <summary>
/// Base for all Exceptions related to Results
/// </summary>
public class ResultsException : Exception
{
    /// <summary>
    /// Constructor with a message
    /// </summary>
    /// <param name="message">the explanation of what caused the exception</param>
    public ResultsException(string message) : base(message) { }
}
