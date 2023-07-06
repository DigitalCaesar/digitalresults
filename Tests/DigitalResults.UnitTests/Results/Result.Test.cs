using System.Reflection;
using DigitalCaesar.Results;
using Newtonsoft.Json.Linq;

namespace DigitalResults.UnitTests.Results;

public class Result_Test
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
    public static IEnumerable<object[]> GetConstructorErrors()
    {
        yield return new object[] { true, new ErrorCollection(new List<Error> { new Error("Test", "Test Error") }) };
        yield return new object[] { false, new ErrorCollection() };
    }
    #endregion 

    #region Constructors
    //[Theory]
    //[MemberData(nameof(GetConstructorErrors))]
    //public void Constructor_Error_Test(bool successful, ErrorCollection errors)
    //{
    //    Type type = typeof(Result);

    //    // Act
    //    Action action = () => Activator.CreateInstance(typeof(Result), successful, errors);

    //    // Assert
    //    Assert.Throws<InvalidOperationException>(action);

    //}
    [Fact]
    public void Constructor_Success_Test()
    {
        // Arrange
        Result TestObject;

        // Act
        TestObject = Result.Success();

        // Assert
        TestObject.Should().NotBeNull();
    }

    [Theory]
    [InlineData("Test Data")]
    [InlineData("")]
    public void Constructor_SuccessWithValue_Test(string testValue)
    {
        // Arrange
        Result TestObject;

        // Act
        TestObject = Result.Success(testValue);

        // Assert
        TestObject.Should().NotBeNull();
    }   
    
    [Theory]
    [MemberData(nameof(GetError))]
    public void Constructor_FailureWithError_Test(Error testError)
    {
        // Arrange
        Result TestObject;

        // Act
        TestObject = Result.Failure(testError);

        // Assert
        TestObject.Should().NotBeNull();
    }
    [Theory]
    [MemberData(nameof(GetErrors))]
    public void Constructor_FailureWithErrors_Test(ErrorCollection testErrors)
    {
        // Arrange
        Result TestObject;

        // Act
        TestObject = Result.Failure(testErrors);

        // Assert
        TestObject.Should().NotBeNull();
    }
    [Theory]
    [MemberData(nameof(GetError))]
    public void Constructor_FailureGenericWithError_Test(Error testError)
    {
        // Arrange
        Result TestObject;

        // Act
        TestObject = Result.Failure<string>(testError);

        // Assert
        TestObject.Should().NotBeNull();
    }
    [Theory]
    [MemberData(nameof(GetErrors))]
    public void Constructor_FailureGenericWithErrors_Test(ErrorCollection testErrors)
    {
        // Arrange
        Result TestObject;

        // Act
        TestObject = Result.Failure<string>(testErrors);

        // Assert
        TestObject.Should().NotBeNull();
    }
    #endregion

    #region Properties
    [Fact]
    public void Property_SuccessfulWhenSuccess_Test()
    {
        // Arrange
        Result TestObject;

        // Act
        TestObject = Result.Success();

        // Assert
        TestObject.Successful.Should().BeTrue();
    }
    [Fact]
    public void Property_SuccessfulWhenFailed_Test()
    {
        // Arrange
        Result TestObject;

        // Act
        TestObject = Result.Failure();

        // Assert
        TestObject.Successful.Should().BeFalse();
    }
    //[Fact]
    //public void Property_ErrorsWhenSuccess_Test()
    //{
    //    // Arrange
    //    Result TestObject;

    //    // Act
    //    TestObject = Result.Success();

    //    // Assert
    //    TestObject.Errors.Should().BeEmpty();
    //}
    //[Fact]
    //public void Property_ErrorsWhenFailed_Test()
    //{
    //    // Arrange
    //    Result TestObject;

    //    // Act
    //    TestObject = Result.Failure();

    //    // Assert
    //    TestObject.Errors.Should().NotBeEmpty();
    //}
    [Fact]
    public void Property_Implicit_Test()
    {
        // Arrange
        Result TestObject;

        // Act
        TestObject = true;

        // Assert
        TestObject.Successful.Should().BeTrue();
    }
    #endregion

}
