using DigitalCaesar.Results.Exceptions;

namespace DigitalResults.UnitTests.Exceptions;

public class ResultsException_Test
{
    [Fact]
    public void Constructor_Test()
    {
        // Arrange
        ResultsException TestObject;

        // Act
        TestObject = new ResultsException("Test Message");

        // Assert 
        TestObject.Should().NotBeNull();
    }


    [Theory]
    [InlineData("Test Message")]
    [InlineData("")]
    public void Method_Message_Test(string testValue)
    {
        // Arrange
        ResultsException TestObject;

        // Act
        TestObject = new ResultsException(testValue);

        // Assert 
        TestObject.Message.Should().Be(testValue);
    }
}
