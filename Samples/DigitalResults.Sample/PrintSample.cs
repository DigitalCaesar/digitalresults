using System.Diagnostics.Metrics;

namespace DigitalCaesar.Results.Sample;

public class PrintSample
{
    public static void PrintResult(TestCase testCase, Result result)
    {
        Console.WriteLine("The result of doing {0} on something simple is '{1}'.", testCase, result.Successful);
    }
    public static void PrintResult<T>(TestCase testCase, Result result)
    {
        PrintResult(testCase, result);
        string Message = result.Match(
            () => GetSuccessMessage(),
            errors => GetFailureMessage(errors));
        Console.WriteLine(Message);
    }
    public static void PrintResult<T>(TestCase testCase, Result<T> result)
    {
        PrintResult(testCase, (Result)result);
        string Message = result.Match(
            value => GetSuccessMessage(value),
            errors => GetFailureMessage(errors));
        Console.WriteLine(Message);
    }
    public static void PrintActionResult(TestCase testCase, Result result)
    {
        PrintResult(testCase, result);
        result.Switch(
            () => PrintSuccessMessage(),
            (errors) => PrintFailureMessages(errors));
    }
    public static void PrintActionResult<T>(TestCase testCase, Result<T> result)
    {
        PrintResult(testCase, (Result)result);
        result.Switch(
            (value) => PrintSuccessMessage(value),
            (errors) => PrintFailureMessages(errors));
    }

    public static string GetSuccessMessage()
    {
        return $"Successfully completed operation.";
    }
    public static string GetSuccessMessage<T>(T? value)
    {
        if(value is not null)
            return $"Successfully computed value '{value}'";
        return $"Successfully completed operation, but value is null.";
    }
    public static string GetFailureMessage(ErrorCollection errors)
    {
        return $"Failed to complete operation due to {errors.Count} error(s).";
    }
    public static string GetFailureMessage(Error error, int position)
    {
        return $"  Error{position}  {error.ErrorType}:{error.Code}:{error.Description}.";
    }

    public static void PrintSuccessMessage()
    {
        Console.WriteLine(GetSuccessMessage());
    }
    public static void PrintSuccessMessage<T>(T value)
    {
        Console.WriteLine(GetSuccessMessage(value));
    }
    public static void PrintFailureMessages(ErrorCollection errors)
    {
        Console.WriteLine(GetFailureMessage(errors));
        int Counter = 1;
        foreach (Error error in errors)
        {
            PrintFailureMessage(error, Counter++);
        }

    }
    public static void PrintFailureMessage(Error error, int position)
    {
        Console.WriteLine(GetFailureMessage(error, position));
    }

}
