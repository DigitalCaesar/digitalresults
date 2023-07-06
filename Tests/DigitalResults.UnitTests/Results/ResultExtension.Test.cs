using System.Reflection;
using DigitalCaesar.Results;
using Newtonsoft.Json.Linq;
using Xunit.Sdk;

namespace DigitalResults.UnitTests.Results;

public class ResultExtension_Test
{
    [Fact]
    public void Method_Ensure_Successful_Test()
    {
        // Arrange
        Result<string> TestObject = "TestValue";

        // Act
        var result = TestObject.Ensure(e => e.Length == 9, Error.Validation);

        // Assert
        result.Successful.Should().BeTrue();
    }
    [Fact]
    public void Method_Ensure_Failed_Test()
    {
        // Arrange
        Result<string> TestObject = "TestValue";

        // Act
        var result = TestObject.Ensure(e => e.Length == 10, Error.Validation);

        // Assert
        result.Successful.Should().BeFalse();
    }

    [Fact]
    public void Method_Map_Successful_Test()
    {
        // Arrange
        Result<string> TestObject = "TestValue";
        string testFunctionResult = "Did not run.";
        string expectedValue = "TestValue";

        // Act
        var result = TestObject.Map(m => testFunctionResult = m);

        // Assert
        result.Successful.Should().BeTrue();
        testFunctionResult.Should().Be(expectedValue);
    }
    [Fact]
    public void Method_Map_Failed_Test()
    {
        // Arrange
        Result<string> TestObject = Error.Validation;
        string testFunctionResult = "Did not run.";
        string expectedValue = "Did not run.";

        // Act
        var result = TestObject.Map(m => testFunctionResult = "TestValue");

        // Assert
        result.Successful.Should().BeFalse();
        testFunctionResult.Should().Be(expectedValue);
    }
}
