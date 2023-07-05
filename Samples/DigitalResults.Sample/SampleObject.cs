namespace DigitalCaesar.Results.Sample;

public class SampleObject
{
    public static Result GetSimpleResult(TestCase doSomething)
    {
        if (doSomething == TestCase.DoSomethingThatWorks)
            return Result.Success();

        return Result.Failure(Errors.SimpleError);
    }

    public static Result<int> GetIntResult(TestCase doSomething)
    {
        if (doSomething == TestCase.DoSomethingThatWorks)
            return Result<int>.Success(10);

        return Result<int>.Failure<int>(Errors.FailedIntegerResult);
    }
    public static Result<string> GetStringResult(TestCase doSomething)
    {
        if (doSomething == TestCase.DoSomethingThatWorks)
            return Result.Success("Test Value");

        return Result.Failure<string>(Errors.FailedStringResult);
    }
}