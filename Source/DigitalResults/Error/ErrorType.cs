namespace DigitalCaesar.Results;

/// <summary>
/// The types of underlying issues that trigger errors
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// An error that does not top a result from being produced
    /// </summary>
    Warning,
    /// <summary>
    /// An error caused by an exception
    /// </summary>
    Exception,
    /// <summary>
    /// An error caused by a validation failure
    /// </summary>
    Validation
}
