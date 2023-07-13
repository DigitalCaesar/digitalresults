# Digital Results

Digital Results makes it possible to create methods with either/or result types for returning success/failure of an operation and either the resulting value or an error based on the state of the Result.  

## Usage

### Creating a Result

Creating a result is as simple as referencing the DigitalCaesar.Results namespace, changing the return type of you method to Result<T> and returning a value and at least one exception from your method.  

```csharp
using DigitalCaesar.Results;

public Result<string> GetResult()
{
    if(Good)
      return "This is a successful result";
    else
      return new Exception("This is a failure result");
}
```

### Using a Result to return a value

To use the Result, use the Match method to retrieve the appropriate value or exception.  Match uses two functions, one for success and one for failure.  The function that matches the state of the Result will be executed and the value returned.

```csharp
var result = GetResult();
string resultMessage = result.Match(
  success => success,
  failure => failure.Message
);
Console.WriteLine(resultMessage);
```

### Using a Result to run an Action

You can also use the Switch method to run different Actions based on the state of the Result.  Switch uses two Actions, one for success and one for failure.  The Action that matches the state of the Result will be executed.

```csharp
var result = GetResult();
result.Switch(
  success => Console.WriteLine(success),
  failure => Console.WriteLine(failure.Message)
);
```

## Documentation

Check out the product page at [DigitalCaesar.com](https://digitalcaesar.com/products/digitalresults) for more information.

## Contributing

Would you like to contribute?  The project is available on [GitHub](https://github.com/DigitalCaesar/digitalresults) and we welcome your pull requests and issues.

## License

Copyright © Digital Caesar LLC