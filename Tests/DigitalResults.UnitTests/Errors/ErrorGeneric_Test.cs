using DigitalCaesar.Results;
using FluentAssertions;

namespace DigitalResults.UnitTests.Errors;
public class ErrorGeneric_Test
{
    private const string cDefaultErrorCode = "TestError";
    private const string cDefaultErrorMessage = "Test Error Message";
    private const string cDefaultErrorData = "Test Data";

    [Fact]
    public void Constructor_Default_Test()
    {
        // Arrange
        Error<string> TestObject;

        // Act
        TestObject = new(cDefaultErrorCode, cDefaultErrorMessage, cDefaultErrorData);

        // Assert
        TestObject.Should().NotBeNull();
    }
    [Fact]
    public void Constructor_Full_Test()
    {
        // Arrange
        Error<string> TestObject;

        // Act
        TestObject = new(cDefaultErrorCode, cDefaultErrorMessage, cDefaultErrorData, ErrorType.Exception);

        // Assert
        TestObject.Should().NotBeNull();
    }
    [Theory]
    [InlineData("TestCode", "TestCode")]
    public void Property_Data_Test(string testValue, string expectedValue)
    {
        // Arrange
        Error<string> TestObject;

        // Act
        TestObject = new(cDefaultErrorCode, cDefaultErrorMessage, testValue);

        // Assert
        TestObject.Data.Should().Be(expectedValue);
    }

}
