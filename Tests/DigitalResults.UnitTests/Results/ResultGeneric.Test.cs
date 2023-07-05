using DigitalCaesar.Results;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace DigitalResults.UnitTests.Results;

public class ResultGeneric_Test
{
    #region Test Data
    public static IEnumerable<object[]> GetError()
    {
        yield return new object[] { new Error("Test", "Test Error") };
        yield return new object[] { new Error(string.Empty, string.Empty) };
    }
    public static IEnumerable<object[]> GetErrors()
    {
        yield return new object[] { new ErrorCollection(new List<Error> { new Error("Test", "Test Error") }) };
        yield return new object[] { new ErrorCollection(new List<Error> { new Error("Test1", "First Test Error"), new Error("Test2", "Second Test Error") } ) };
    }
    #endregion 

    #region Constructors
    #endregion

    #region Properties
    [Theory]
    [InlineData("")]
    [InlineData("TestValue")]
    public void Property_Value_Test(string testValue)
    {
        // Arrange
        Result<string> TestObject;

        // Act
        TestObject = Result.Success(testValue);

        // Assert
        TestObject.Value.Should().NotBeNull();
        TestObject.Value.Should().Be(testValue);
    }

    [Fact]
    public void Property_Implicit_Test()
    {
        // Arrange
        string TestValue = "Test";
        Result<string> TestObject;

        // Act
        TestObject = TestValue;

        // Assert
        TestObject.Successful.Should().BeTrue();
        TestObject.Errors.Should().BeEmpty();
        TestObject.Value.Should().Be(TestValue);
    }
    #endregion

    #region Methods
    [Fact]
    public void Method_MatchSuccess_Test()
    {
        // Arrange
        Result<string> TestObject = Result.Success("Test");

        // Act
        string result = TestObject.Match(
            success => "Successful Test",
            failed => "Failed Test");

        // Assert
        result.Should().Be("Successful Test");
    }
    [Fact]
    public void Method_MatchFailure_Test()
    {
        // Arrange
        Result<string> TestObject = Result.Failure<string>(Error.NullValue);

        // Act
        string result = TestObject.Match(
            success => "Successful Test",
            failed => "Failed Test");

        // Assert
        result.Should().Be("Failed Test");
    }
    #endregion

}
