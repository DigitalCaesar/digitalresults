using System.Collections.ObjectModel;

namespace DigitalCaesar.Results;

/// <summary>
/// Aggregates multiple Results with generic values allowing the overall results to be evaluated together
/// </summary>
public class AggregateResult<T>
{
    private readonly List<Result<T>> mResults;

    /// <summary>
    /// A list of the related results
    /// </summary>
    public ReadOnlyCollection<Result<T>> Results => mResults.AsReadOnly();

    /// <summary>
    /// Default constructor initializes an empty list of results
    /// </summary>
    public AggregateResult()
    {
        mResults = new();
    }
    /// <summary>
    /// Constructor that takes a list of results to be included in the aggregate list of results
    /// </summary>
    /// <param name="results">a list of errors to include</param>
    public AggregateResult(List<Result<T>> results)
    {
        mResults = new(results);
    }

    /// <summary>
    /// Evaluates the aggregate result such that any included result with a failure will cause the aggregate to reflect a failure
    /// </summary>
    /// <returns>a boolean where true is successful and false is failure</returns>
    public bool GetSuccessStatus()
    {
        foreach (var result in Results)
        {
            if (!result.Successful)
                return false;
        }
        return true;
    }
    /// <summary>
    /// Aggregates allow the the Results Errors into one list
    /// </summary>
    /// <returns>an collection of Errors</returns>
    public ErrorCollection GetErrors()
    {
        List<Error> errors = new();
        foreach(var result in Results)
        {
            List<Error> ResultErrors = result.Match(
                () => new List<Error>(), 
                error => error.ToList());
            errors.AddRange(ResultErrors);
        }
        return new(errors);
    }
    /// <summary>
    /// Adds a single Result to the list of resutls
    /// </summary>
    /// <param name="result">a Result to add</param>
    public void Add(Result<T> result)
    {
        mResults.Add(result);
    }
}
