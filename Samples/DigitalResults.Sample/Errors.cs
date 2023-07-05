namespace DigitalCaesar.Results.Sample; 

public class Errors  
{
    public static Error SimpleError => new("0001", "Sample Error for simple Result", ErrorType.Warning);
    public static Error FailedIntegerResult => new("0002", "Sample Error for Result", ErrorType.Warning);
    public static Error FailedStringResult => new("0003", "Sample Error for Result", ErrorType.Warning);
}
