using DigitalCaesar.Results;
using Xunit.Sdk;

namespace DigitalResults.UnitTests.Aggregate;

public class AggregateResultGeneric_Test
{
    #region TestData
    private const string cDefaultValue = "Test";
    private static readonly Error cDefaultError = Error.Unknown;
    public static IEnumerable<object[]> GetConstructorResults()
    {
        yield return new object[] { new List<Result<string>> { Result.Success(cDefaultValue) } };
        yield return new object[] { new List<Result<string>> { Result.Failure<string>(cDefaultError) } };
        yield return new object[] { new List<Result<string>> { Result.Success(cDefaultValue), Result.Failure<string>(cDefaultError) } };
        yield return new object[] { new List<Result<string>>() };
    }
    public static IEnumerable<object[]> GetSuccess()
    {
        yield return new object[] { new List<Result<string>> { Result.Success(cDefaultValue) }, true };
        yield return new object[] { new List<Result<string>> { Result.Failure<string>(cDefaultError) }, false };
        yield return new object[] { new List<Result<string>> { Result.Success(cDefaultValue), Result.Failure<string>(cDefaultError) }, false };
        yield return new object[] { new List<Result<string>>(), true };
    }
    public static IEnumerable<object[]> GetErrors()
    {
        yield return new object[] { new List<Result<string>> { Result.Success(cDefaultValue) }, new ErrorCollection() };
        yield return new object[] { new List<Result<string>> { Result.Failure<string>(cDefaultError) }, new ErrorCollection(cDefaultError) };
        yield return new object[] { new List<Result<string>> { Result.Success(cDefaultValue), Result.Failure<string>(cDefaultError) }, new ErrorCollection(cDefaultError) };
        yield return new object[] { new List<Result<string>>(), new ErrorCollection() };
    }
    public static IEnumerable<object[]> AddResult()
    {
        yield return new object[] { new List<Result<string>> { Result.Success(cDefaultValue) }, Result.Success(cDefaultValue), 2, 0, true };
        yield return new object[] { new List<Result<string>> { Result.Success(cDefaultValue) }, Result.Failure<string>(cDefaultError), 2, 1, false };
        yield return new object[] { new List<Result<string>> { Result.Failure<string>(cDefaultError) }, Result.Failure<string>(cDefaultError), 2, 2, false };
        yield return new object[] { new List<Result<string>> { Result.Failure<string>(cDefaultError) }, Result.Success(cDefaultValue), 2, 1, false };
        yield return new object[] { new List<Result<string>> { Result.Success(cDefaultValue), Result.Failure<string>(cDefaultError) }, Result.Success(cDefaultValue), 3, 1, false };
        yield return new object[] { new List<Result<string>>(), Result.Success(cDefaultValue), 1, 0, true };
        yield return new object[] { new List<Result<string>>(), Result.Failure<string>(cDefaultError), 1, 1, false };
    }
    #endregion

    #region Constructors
    [Fact]
    public void Constructor_Test()
    {
        // Arrange
        AggregateResult<string> TestObject; 

        // Act
        TestObject = new AggregateResult<string>();

        // Assert 
        TestObject.Should().NotBeNull();
    }

    [Theory]
    [MemberData(nameof(GetConstructorResults))]
    public void Constructor_WithList_Test(List<Result<string>> testResults)
    {
        // Arrange
        AggregateResult<string> TestObject;

        // Act
        TestObject = new AggregateResult<string>(testResults);

        // Assert 
        TestObject.Should().NotBeNull();
    }
    #endregion

    #region Properties
    [Theory]
    [MemberData(nameof(GetConstructorResults))]
    public void Results_Test(List<Result<string>> testResults)
    {
        // Arrange
        AggregateResult<string> TestObject;

        // Act
        TestObject = new AggregateResult<string>(testResults);

        // Assert 
        TestObject.Results.Should().NotBeNull();
        TestObject.Results.Count.Should().Be(testResults.Count);
        if(testResults.Count > 0) 
            TestObject.Results.Should().Contain(testResults);
    }
    #endregion

    #region Methods
    [Theory]
    [MemberData(nameof(GetSuccess))]
    public void GetSuccessStatus_Test(List<Result<string>> testResults, bool expectedSuccessful)
    {
        // Arrange
        AggregateResult<string> TestObject = new(testResults);

        // Act
        bool result = TestObject.GetSuccessStatus();

        // Assert 
        result.Should().Be(expectedSuccessful);
    }
    [Theory]
    [MemberData(nameof(GetErrors))]
    public void GetErrors_Test(List<Result<string>> testResults, ErrorCollection expectedValue)
    {
        // Arrange
        AggregateResult<string> TestObject = new(testResults);

        // Act
        var result = TestObject.GetErrors();

        // Assert 
        result.Count.Should().Be(expectedValue.Count);
        if(result.Count > 0)
            result.Should().Contain(expectedValue);
    }
    [Theory]
    [MemberData(nameof(AddResult))]
    public void AddResult_Test(List<Result<string>> testResults, Result<string> newResult, int expectedResultCount, int expectedErrorCount, bool expectedSuccessful)
    {
        // Arrange
        AggregateResult<string> TestObject = new(testResults);

        // Act
        TestObject.Add(newResult);

        // Assert 
        if(TestObject.Results.Count > 0 && testResults.Count > 0)
            TestObject.Results.Should().Contain(testResults);
        TestObject.GetSuccessStatus().Should().Be(expectedSuccessful);
        TestObject.Results.Should().HaveCount(expectedResultCount);
        TestObject.GetErrors().Should().HaveCount(expectedErrorCount);
    }
    #endregion
}
