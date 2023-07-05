using System.Collections.ObjectModel;

namespace DigitalCaesar.Results;

public class ErrorCollection : ReadOnlyCollection<Error>
{
    public ErrorCollection() : base(new List<Error>())
    {
    }
    public ErrorCollection(Error error) : base(new List<Error>() { error })
    {
    }
    public ErrorCollection(IList<Error> list) : base(list)
    {
    }

    public bool IsEmpty => Count < 1;
    public bool IsSingle => Count == 1;
}
