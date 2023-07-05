// See https://aka.ms/new-console-template for more information
using DigitalCaesar.Results;
using DigitalCaesar.Results.Sample;


// Without a match, you are required to test the outcome before using the value?
Result SimpleResult;
SimpleResult = SampleObject.GetSimpleResult(TestCase.DoSomethingThatWorks);
PrintSample.PrintResult(TestCase.DoSomethingThatWorks, SimpleResult);

SimpleResult = SampleObject.GetSimpleResult(TestCase.DoSomethingThatFails);
PrintSample.PrintResult(TestCase.DoSomethingThatFails, SimpleResult);

// INT test
Result<int> IntResult;
IntResult = SampleObject.GetIntResult(TestCase.DoSomethingThatWorks);
PrintSample.PrintActionResult(TestCase.DoSomethingThatWorks, IntResult);

IntResult = SampleObject.GetIntResult(TestCase.DoSomethingThatFails);
PrintSample.PrintActionResult(TestCase.DoSomethingThatFails, IntResult);

// STRING test
Result<string> StringResult;
StringResult = SampleObject.GetStringResult(TestCase.DoSomethingThatWorks);
PrintSample.PrintActionResult(TestCase.DoSomethingThatWorks, StringResult);

StringResult = SampleObject.GetStringResult(TestCase.DoSomethingThatFails);
PrintSample.PrintActionResult(TestCase.DoSomethingThatFails, StringResult);
