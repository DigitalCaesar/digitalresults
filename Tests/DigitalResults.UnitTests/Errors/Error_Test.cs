﻿using DigitalCaesar.Results;
using FluentAssertions;

namespace DigitalResults.UnitTests.Errors;

public class Error_Test
{
    private const string cDefaultErrorCode = "TestError";
    private const string cDefaultErrorMessage = "Test Error Message";

    [Fact]
    public void Error_None_Test()
    {
        // Arrange
        Error TestObject;

        // Act
        TestObject = Error.None;

        // Assert
        TestObject.Code.Should().BeEmpty();
        TestObject.Description.Should().BeEmpty();
        TestObject.ErrorType.Should().Be(ErrorType.Warning);
    }

    [Fact]
    public void Error_Null_Test()
    {
        // Arrange
        Error TestObject;

        // Act
        TestObject = Error.NullValue;

        // Assert
        TestObject.Code.Should().Be("Error.NullValue");
        TestObject.Description.Should().Be("The specified result value is null.");
        TestObject.ErrorType.Should().Be(ErrorType.Warning);
    }

    [Fact]
    public void Error_Validation_Test()
    {
        // Arrange
        Error TestObject;

        // Act
        TestObject = Error.Validation;

        // Assert
        TestObject.Code.Should().Be("ValidationError");
        TestObject.Description.Should().Be("A validation error occurred.");
        TestObject.ErrorType.Should().Be(ErrorType.Warning);
    }

    [Fact]
    public void Constructor_Default_Test()
    {
        // Arrange
        Error TestObject;

        // Act
        TestObject = new(cDefaultErrorCode, cDefaultErrorMessage);

        // Assert
        TestObject.Should().NotBeNull();
    }
    [Fact]
    public void Constructor_Full_Test()
    {
        // Arrange
        Error TestObject;

        // Act
        TestObject = new(cDefaultErrorCode, cDefaultErrorMessage, ErrorType.Exception);

        // Assert
        TestObject.Should().NotBeNull();
    }
    [Theory]
    [InlineData("TestCode", "TestCode")]
    public void Property_Code_Test(string testValue, string expectedValue)
    {
        // Arrange
        Error TestObject;

        // Act
        TestObject = new(testValue, cDefaultErrorMessage);

        // Assert
        TestObject.Code.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("TestDescription", "TestDescription")]
    public void Property_Description_Test(string testValue, string expectedValue)
    {
        // Arrange
        Error TestObject;

        // Act
        TestObject = new(cDefaultErrorCode, testValue);

        // Assert
        TestObject.Description.Should().Be(expectedValue);
    }
    [Theory]
    [InlineData(ErrorType.Warning)]
    [InlineData(ErrorType.Validation)]
    [InlineData(ErrorType.Exception)]
    public void Property_ErrorType_Test(ErrorType errorType)
    {
        // Arrance
        Error TestObject;

        // Act
        TestObject = new(cDefaultErrorCode, cDefaultErrorMessage, errorType);
        
        // Assert
        TestObject.ErrorType.Should().Be(errorType);
    }
    [Fact]
    public void Property_ErrorType_Default_Test()
    {
        // Arrance
        Error TestObject;

        // Act
        TestObject = new(cDefaultErrorCode, cDefaultErrorMessage);

        // Assert
        TestObject.ErrorType.Should().Be(ErrorType.Warning);
    }
}
