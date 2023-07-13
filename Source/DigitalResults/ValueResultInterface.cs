namespace DigitalCaesar.Results;

/// <summary>
/// Defines a Result that can be successful or failed and holds a result Value
/// </summary>
public interface IValueResult<T> : IResult
{
    /// <summary>
    /// Matches the appropriate response based on the state of the Result
    /// </summary>
    /// <typeparam name="R">The type of value to return</typeparam>
    /// <param name="onSuccess">the function to execute if the Result is in a success state</param>
    /// <param name="onFailure">the function to execute if the Result is in a failure state</param>
    /// <returns>Either a Value if successful or an collection of Errors if failed</returns>
    R Match<R>(Func<T, R> onSuccess, Func<ErrorCollection, R> onFailure);

    /// <summary>
    /// Switches between Actions dependent on the state of the Result
    /// </summary>
    /// <param name="onSuccess">the Action to execute if the Result is in a success state</param>
    /// <param name="onFailure">the Action to execute if the Result is in a failure state</param>
    void Switch(Action<T> onSuccess, Action<ErrorCollection> onFailure);
}