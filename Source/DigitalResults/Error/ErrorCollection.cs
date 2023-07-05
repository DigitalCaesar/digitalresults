using System.Collections.ObjectModel;

namespace DigitalCaesar.Results;

/// <summary>
/// A read only list of errors intended to be immutable for Results
/// </summary>
public class ErrorCollection : ReadOnlyCollection<Error>
{
    /// <summary>
    /// Constructs an empty collection
    /// </summary>
    public ErrorCollection() : base(new List<Error>())
    {
    }
    /// <summary>
    /// Constructs a collection of errors from a single error
    /// </summary>
    /// <param name="error">an single error to populate the collection</param>
    public ErrorCollection(Error error) : base(new List<Error>() { error })
    {
    }
    /// <summary>
    /// Creates a collection of errors from a list of errors
    /// </summary>
    /// <param name="list">the source list of errors</param>
    public ErrorCollection(IList<Error> list) : base(list)
    {
    }

    /// <summary>
    /// True if the collection is empty or False if the collection has errors
    /// </summary>
    public bool IsEmpty => Count < 1;
    /// <summary>
    /// True if there is only one error, denotes a single or multiple errors in the list.  
    /// </summary>
    public bool IsSingle => Count == 1;
}
