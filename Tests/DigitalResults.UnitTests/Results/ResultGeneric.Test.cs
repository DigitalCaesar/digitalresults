﻿using DigitalCaesar.Results;
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

    [Fact]
    public void Property_ImplicitValue_Test()
    {
        // Arrange
        string TestValue = "Test";
        Result<string> TestObject;

        // Act
        TestObject = TestValue;

        // Assert
        TestObject.Successful.Should().BeTrue();
    }
    [Fact]
    public void Property_ImplicitError_Test()
    {
        // Arrange
        Error TestValue = Error.NullValue;
        Result<string> TestObject;

        // Act
        TestObject = TestValue;

        // Assert
        TestObject.Successful.Should().BeFalse();
    }
    [Fact]
    public void Property_ImplicitErrorCollection_Test()
    {
        // Arrange
        ErrorCollection TestValue = new(Error.NullValue);
        Result<string> TestObject;

        // Act
        TestObject = TestValue;

        // Assert
        TestObject.Successful.Should().BeFalse();
    }
    #endregion

    #region Methods
    [Fact]
    public void Method_SwitchSuccess_Test()
    {
        // Arrange
        Result<string> TestObject = Result.Success("Test");
        string result = "Did not run";

        // Act
        TestObject.Switch(
            (success) => result = "Successful Test",
            (failed) => result = "Failed Test");

        // Assert
        result.Should().Be("Successful Test");
    }
    [Fact]
    public void Method_SwitchFailure_Test()
    {
        // Arrange
        Result<string> TestObject = Result.Failure<string>(Error.NullValue);
        string result = "Did not run";
        // Act
        TestObject.Switch(
            (success) => result = "Successful Test",
            (failed) => result = "Failed Test");

        // Assert
        result.Should().Be("Failed Test");
    }
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
