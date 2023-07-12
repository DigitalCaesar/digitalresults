namespace DigitalCaesar.Results;

public interface IResult
{
    bool Successful { get; }

    R Match<R>(Func<R> success, Func<ErrorCollection, R> failure);
    void Switch(Action onSuccess, Action<ErrorCollection> onError);
}