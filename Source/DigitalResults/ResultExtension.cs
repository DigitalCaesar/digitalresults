﻿namespace DigitalCaesar.Results;

/// <summary>
/// Extension allows for a Result to be evaluated against validation citeria and mapped to a new result
/// </summary>
public static class ResultExtension
{
    /// <summary>
    /// Tests a result against validation criteria
    /// </summary>
    /// <typeparam name="T">The value type of the result</typeparam>
    /// <param name="result">a result to evaluate</param>
    /// <param name="predicate">the function to use for validation</param>
    /// <param name="error">the error to return if validation fails</param>
    /// <returns>The original success result if successful or a failure result if failed</returns>
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
    {
        if (!result.Successful)
            return result;
        
        return ((result.Value is not null) && predicate(result.Value))
            ? result
            : Result.Failure<T>(error);
    }

    /// <summary>
    /// Maps a function to result using the value of a result
    /// </summary>
    /// <typeparam name="TIn">The type of the input value</typeparam>
    /// <typeparam name="TOut">The type of the return value</typeparam>
    /// <param name="result">The result to map</param>
    /// <param name="mappingFunc">The function to evaluate</param>
    /// <returns>A success result if no errors otherwise a failure result</returns>
    public static Result<TOut> Map<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> mappingFunc)
    {
        return ((result.Value is not null) && (result.Successful))
            ? Result.Success(mappingFunc(result.Value))
            : Result.Failure<TOut>(result.Errors);
    }
}
