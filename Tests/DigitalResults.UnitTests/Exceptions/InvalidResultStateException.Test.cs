using DigitalCaesar.Results.Exceptions;

namespace DigitalResults.UnitTests.Exceptions;

public class InvalidResultStateException_Test
{
    [Fact]
    public void Constructor_Failure_Test()
    {
        // Arrange
        InvalidResultStateException TestObject;

        // Act
        TestObject = InvalidResultStateException.FailureException;

        // Assert 
        TestObject.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_Success_Test()
    {
        // Arrange
        InvalidResultStateException TestObject;

        // Act
        TestObject = InvalidResultStateException.SuccessException;

        // Assert 
        TestObject.Should().NotBeNull();
    }
    [Fact]
    public void Constructor_Value_Test()
    {
        // Arrange
        InvalidResultStateException TestObject;

        // Act
        TestObject = InvalidResultStateException.ValueException;

        // Assert 
        TestObject.Should().NotBeNull();
    }
}
