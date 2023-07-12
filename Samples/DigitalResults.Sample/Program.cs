using DigitalCaesar.Results;
using DigitalCaesar.Results.Sample;


// Without a match, you are required to test the outcome before using the value?
Console.WriteLine("Simple Result test with no value");
Console.WriteLine("--------------------------------------------------");
Result SimpleResult;
Console.WriteLine("Test Success");
SimpleResult = SampleObject.GetSimpleResult(TestCase.DoSomethingThatWorks);
PrintSample.PrintResult(TestCase.DoSomethingThatWorks, SimpleResult);
Console.WriteLine("Test Failure");
SimpleResult = SampleObject.GetSimpleResult(TestCase.DoSomethingThatFails);
PrintSample.PrintResult(TestCase.DoSomethingThatFails, SimpleResult);

// INT test
Console.WriteLine("Simple Result test with int value");
Console.WriteLine("--------------------------------------------------");
Result<int> IntResult;
Console.WriteLine("Test Success");
IntResult = SampleObject.GetIntResult(TestCase.DoSomethingThatWorks);
PrintSample.PrintActionResult(TestCase.DoSomethingThatWorks, IntResult);
Console.WriteLine("Test Failure");
IntResult = SampleObject.GetIntResult(TestCase.DoSomethingThatFails);
PrintSample.PrintActionResult(TestCase.DoSomethingThatFails, IntResult);

// STRING test
Console.WriteLine("Simple Result test with string value");
Console.WriteLine("--------------------------------------------------");
Result<string> StringResult;
Console.WriteLine("Test Success");
StringResult = SampleObject.GetStringResult(TestCase.DoSomethingThatWorks);
PrintSample.PrintActionResult(TestCase.DoSomethingThatWorks, StringResult);
Console.WriteLine("Test Failure");
StringResult = SampleObject.GetStringResult(TestCase.DoSomethingThatFails);
PrintSample.PrintActionResult(TestCase.DoSomethingThatFails, StringResult);


// Traditionl Example
Console.WriteLine("Traditionl Example");
Console.WriteLine("--------------------------------------------------");
TraditionalResult(10, 3); // Result: 3.3333333333333333333333333333
TraditionalResult(3, 10); // Result: 0.3
TraditionalResult(0, 10); // Result: 0
TraditionalResult(0, 0);  // DivideByZeroException
TraditionalResult(10, 0); // DivideByZeroException

decimal GetAverage(int sum, int count)
{
    return (decimal)sum / (decimal)count;
}

void TraditionalResult(int sum, int count)
{
    try
    {
        var result = GetAverage(sum, count);
        Console.WriteLine($"The average is {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Failed to calculate the average due to {0}", ex.Message);
    }
}

// MatchedResult Example
Console.WriteLine("SwitchedResult Example");
Console.WriteLine("--------------------------------------------------");
MatchedResult(10, 3); // Result: 3.3333333333333333333333333333
MatchedResult(3, 10); // Result: 0.3
MatchedResult(0, 10); // Result: 0
MatchedResult(0, 0);  // Error:DivideByZeroException
MatchedResult(10, 0); // Error:DivideByZeroException

Result<decimal> GetAverageAsResult(int sum, int count)
{
    try
    {
        return (decimal)sum / (decimal)count;
    }
    catch (Exception ex)
    {
        return new ExceptionError(ex);
    }
}
void MatchedResult(int sum, int count)
{
    var result = GetAverageAsResult(sum, count);
    var message = result.Match(
        success => $"The average is {success}",
        failure => $"Failed to calculate the average due to {failure[0].Description}."
    );
    Console.WriteLine(message);
}



// SwitchedResult Example
Console.WriteLine("SwitchedResult Example");
Console.WriteLine("--------------------------------------------------");
SwitchedResult(10, 3); // Result: 3.3333333333333333333333333333
SwitchedResult(3, 10); // Result: 0.3
SwitchedResult(0, 10); // Result: 0
SwitchedResult(0, 0);  // Error:DivideByZeroException
SwitchedResult(10, 0); // Error:DivideByZeroException


Result<decimal> GetValidatedAverageAsResult(int sum, int count)
{
    if (count <= 0)
        return new Error("Bad Count", $"The count '{count}' must be greater than 0");
    if (count > 100)
        return new Error("Bad Count", $"The count '{count}' must be less than 100");

    try
    {
        return (decimal)sum / (decimal)count;
    }
    catch (Exception ex)
    {
        return ex;
    }
}
void SwitchedResult(int sum, int count)
{
    var result = GetAverageAsResult(sum, count);
    result.Switch(
        success => Console.WriteLine($"The average is {success}"),
        failure => Console.WriteLine($"Failed to calculate the average due to {failure[0].Description}.")
    );
}
