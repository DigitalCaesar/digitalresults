using System.Security.Cryptography;
using DigitalCaesar.Results;

namespace DigitalResults.UnitTests.Errors;

public class ErrorCollection_Test
{

    public static IEnumerable<object[]> GetError()
    {
        yield return new object[] { new Error("Test", "Test Error") };
        yield return new object[] { new Error(string.Empty, string.Empty) };
    }
    public static IEnumerable<object[]> GetErrors()
    {
        yield return new object[] { new List<Error> { new Error("Test", "Test Error") } };
        yield return new object[] { new List<Error> { new Error("Test1", "First Test Error"), new Error("Test2", "Second Test Error") } };
    }
    public static IEnumerable<object[]> GetErrorsForEmptyTest()
    {
        yield return new object[] { new List<Error>(), true };
        yield return new object[] { new List<Error> { new Error("Test", "Test Error") }, false };
        yield return new object[] { new List<Error> { new Error("Test1", "First Test Error"), new Error("Test2", "Second Test Error") }, false };
    }
    public static IEnumerable<object[]> GetErrorsForSingleTest()
    {
        yield return new object[] { new List<Error>(), false };
        yield return new object[] { new List<Error> { new Error("Test", "Test Error") }, true };
        yield return new object[] { new List<Error> { new Error("Test1", "First Test Error"), new Error("Test2", "Second Test Error") }, false };
    }

    [Fact]
    public void Constructor_Test()
    {
        // Arrange
        ErrorCollection TestObject;

        // Act
        TestObject = new ErrorCollection();

        // Assert
        TestObject.Should().NotBeNull();
    }

    [Theory]
    [MemberData(nameof(GetError))]
    public void Constructor_Error_Test(Error testError)
    {
        // Arrange
        ErrorCollection TestObject;

        // Act
        TestObject = new ErrorCollection(testError);

        // Assert
        TestObject.Should().NotBeNull();
    }

    [Theory]
    [MemberData(nameof(GetErrors))]
    public void Constructor_Errors_Test(List<Error> errors)
    {
        // Arrange
        ErrorCollection TestObject;

        // Act
        TestObject = new ErrorCollection(errors);

        // Assert
        TestObject.Should().NotBeNull();
    }

    [Theory]
    [MemberData(nameof(GetErrorsForEmptyTest))]
    public void Method_IsEmpty_Test(List<Error> errors, bool expectedValue)
    {
        // Arrange
        ErrorCollection TestObject;

        // Act
        TestObject = new(errors);

        // Assert
        TestObject.IsEmpty.Should().Be(expectedValue);
    }

    [Theory]
    [MemberData(nameof(GetErrorsForSingleTest))]
    public void Method_IsSingle_Test(List<Error> errors, bool expectedValue)
    {
        // Arrange
        ErrorCollection TestObject;

        // Act
        TestObject = new(errors);

        // Assert
        TestObject.IsSingle.Should().Be(expectedValue);
    }
}
