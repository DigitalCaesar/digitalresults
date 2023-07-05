namespace DigitalCaesar.Results.Sample;

public class PrintSample
{
    public static void PrintResult(TestCase testCase, Result result)
    {
        Console.WriteLine("The result of doing {0} on something simple is '{1}'.", testCase, result.Successful);
    }
    public static void PrintResult<T>(TestCase testCase, Result<T> result)
    {
        PrintResult(testCase, result as Result);
        string Message = result.Match(
            value => $"Successfully computed value '{value}'",
            error => $"Failed to compute value due to {error.Count} error(s).");//'{error.ErrorType}:{error.Code}:{error.Description}'.");
        Console.WriteLine(Message);
    }
    public static void PrintActionResult<T>(TestCase testCase, Result<T> result)
    {
        PrintResult(testCase, result as Result);
        result.Switch(
            (value) => Console.WriteLine("Successfully computed value '{0}'.", value),
            (errors) =>
            {
                Console.WriteLine("Failed to compute value due to {0} error(s).", errors.Count);
                int Counter = 1;
                foreach (Error error in errors)
                {
                    Console.WriteLine("  Error{0}  {1}:{2}:{3}.", Counter++, error.ErrorType, error.Code, error.Description);
                }
            });
    }
}
