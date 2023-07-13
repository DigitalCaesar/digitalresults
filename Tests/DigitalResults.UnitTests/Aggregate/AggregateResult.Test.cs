using DigitalCaesar.Results;
using Xunit.Sdk;

namespace DigitalResults.UnitTests.Aggregate;

public class AggregateResult_Test
{
    #region TestData
    public static IEnumerable<object[]> GetConstructorResults()
    {
        yield return new object[] { new List<Result> { Result.Success() } };
        yield return new object[] { new List<Result> { Result.Failure() } };
        yield return new object[] { new List<Result> { Result.Success(), Result.Failure() } };
        yield return new object[] { new List<Result>() };
    }
    public static IEnumerable<object[]> GetSuccess()
    {
        yield return new object[] { new List<Result> { Result.Success() }, true };
        yield return new object[] { new List<Result> { Result.Failure() }, false };
        yield return new object[] { new List<Result> { Result.Success(), Result.Failure() }, false };
        yield return new object[] { new List<Result>(), true };
    }
    public static IEnumerable<object[]> GetErrors()
    {
        yield return new object[] { new List<Result> { Result.Success() }, new ErrorCollection() };
        yield return new object[] { new List<Result> { Result.Failure(Error.NullValue) }, new ErrorCollection(Error.NullValue) };
        yield return new object[] { new List<Result> { Result.Success(), Result.Failure() }, new ErrorCollection(Error.Unknown) };
        yield return new object[] { new List<Result>(), new ErrorCollection() };
    }
    public static IEnumerable<object[]> AddResult()
    {
        yield return new object[] { new List<Result> { Result.Success() }, Result.Success(), 2, 0, true };
        yield return new object[] { new List<Result> { Result.Success() }, Result.Failure(), 2, 1, false };
        yield return new object[] { new List<Result> { Result.Failure() }, Result.Failure(), 2, 2, false };
        yield return new object[] { new List<Result> { Result.Failure() }, Result.Success(), 2, 1, false };
        yield return new object[] {new List<Result> { Result.Success(), Result.Failure() }, Result.Success(), 3, 1, false };
        yield return new object[] { new List<Result>(), Result.Success(), 1, 0, true };
        yield return new object[] { new List<Result>(), Result.Failure(), 1, 1, false };
    }
    #endregion

    #region Constructors
    [Fact]
    public void Constructor_Test()
    {
        // Arrange
        AggregateResult TestObject; 

        // Act
        TestObject = new AggregateResult();

        // Assert 
        TestObject.Should().NotBeNull();
    }

    [Theory]
    [MemberData(nameof(GetConstructorResults))]
    public void Constructor_WithList_Test(List<Result> testResults)
    {
        // Arrange
        AggregateResult TestObject;

        // Act
        TestObject = new AggregateResult(testResults);

        // Assert 
        TestObject.Should().NotBeNull();
    }
    #endregion

    #region Properties
    [Theory]
    [MemberData(nameof(GetConstructorResults))]
    public void Results_Test(List<Result> testResults)
    {
        // Arrange
        AggregateResult TestObject;

        // Act
        TestObject = new AggregateResult(testResults);

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
    public void GetSuccessStatus_Test(List<Result> testResults, bool expectedSuccessful)
    {
        // Arrange
        AggregateResult TestObject = new(testResults);

        // Act
        bool result = TestObject.GetSuccessStatus();

        // Assert 
        result.Should().Be(expectedSuccessful);
    }
    [Theory]
    [MemberData(nameof(GetErrors))]
    public void GetErrors_Test(List<Result> testResults, ErrorCollection expectedValue)
    {
        // Arrange
        AggregateResult TestObject = new(testResults);

        // Act
        var result = TestObject.GetErrors();

        // Assert 
        result.Count.Should().Be(expectedValue.Count);
        if(result.Count > 0)
            result.Should().Contain(expectedValue);
    }
    [Theory]
    [MemberData(nameof(AddResult))]
    public void AddResult_Test(List<Result> testResults, Result newResult, int expectedResultCount, int expectedErrorCount, bool expectedSuccessful)
    {
        // Arrange
        AggregateResult TestObject = new(testResults);

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
