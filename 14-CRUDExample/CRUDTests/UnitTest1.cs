namespace CRUDTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var calculator = new Calculator();

        int x = 1;
        int y = 2;

        var expected = 3;

        // Act
        var actual = calculator.Add(x, y);

        // Assert
        Assert.Equal(expected, actual);
    }
}