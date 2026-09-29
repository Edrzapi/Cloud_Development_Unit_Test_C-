using Exercises.Exercise1;

namespace Exercises.Solutions;

/// <summary>
/// EXERCISE 1 SOLUTION. Trainer copy, all tests implemented and passing.
/// </summary>
public class Exercise1_CalculatorTests
{
    private readonly Calculator _calculator = new Calculator();

    // ------------------------------------------------------------------ Add

    [Fact]
    public void Add_TwoSmallNumbers_ReturnsTheirSum()
    {
        // Arrange
        double num1 = 10;
        double num2 = 30;

        // Act
        double actual = _calculator.Add(num1, num2);

        // Assert
        Assert.Equal(40, actual);
    }

    [Fact]
    public void Add_TwoNegativeNumbers_ReturnsNegativeSum()
    {
        // Arrange / Act
        double actual = _calculator.Add(-10, -30);

        // Assert
        Assert.Equal(-40, actual);
    }

    [Fact]
    public void Add_MaxValueToMaxValue_ReturnsPositiveInfinity()
    {
        // Arrange / Act: the largest double plus itself cannot be represented.
        double actual = _calculator.Add(double.MaxValue, double.MaxValue);

        // Assert
        Assert.Equal(double.PositiveInfinity, actual);
    }

    [Fact]
    public void Add_TwoSmallestPositiveValues_ReturnsTwiceEpsilon()
    {
        // Arrange: double.Epsilon is the smallest positive double above zero.
        double actual = _calculator.Add(double.Epsilon, double.Epsilon);

        // Assert
        Assert.Equal(double.Epsilon * 2, actual);
    }

    [Fact]
    public void Add_MaxValueToMinValue_ReturnsZero()
    {
        // Arrange: double.MinValue is the most negative double, not the smallest positive one.
        double actual = _calculator.Add(double.MaxValue, double.MinValue);

        // Assert
        Assert.Equal(0, actual);
    }

    // ------------------------------------------------------------------ Subtract

    [Fact]
    public void Subtract_SmallerFromLarger_ReturnsPositiveDifference()
    {
        double actual = _calculator.Subtract(30, 10);

        Assert.Equal(20, actual);
    }

    [Fact]
    public void Subtract_LargerFromSmaller_ReturnsNegativeResult()
    {
        double actual = _calculator.Subtract(10, 30);

        Assert.Equal(-20, actual);
    }

    [Fact]
    public void Subtract_NumberFromItself_ReturnsZero()
    {
        double actual = _calculator.Subtract(42.5, 42.5);

        Assert.Equal(0, actual);
    }

    [Fact]
    public void Subtract_MaxValueFromMinValue_ReturnsNegativeInfinity()
    {
        double actual = _calculator.Subtract(double.MinValue, double.MaxValue);

        Assert.Equal(double.NegativeInfinity, actual);
    }

    // ------------------------------------------------------------------ Multiply

    [Fact]
    public void Multiply_TwoNormalNumbers_ReturnsProduct()
    {
        double actual = _calculator.Multiply(6, 7);

        Assert.Equal(42, actual);
    }

    [Fact]
    public void Multiply_AnyNumberByZero_ReturnsZero()
    {
        double actual = _calculator.Multiply(12345.678, 0);

        Assert.Equal(0, actual);
    }

    [Fact]
    public void Multiply_TwoNegativeNumbers_ReturnsPositiveProduct()
    {
        double actual = _calculator.Multiply(-6, -7);

        Assert.Equal(42, actual);
    }

    [Fact]
    public void Multiply_MaxValueByTwo_ReturnsPositiveInfinity()
    {
        double actual = _calculator.Multiply(double.MaxValue, 2);

        Assert.Equal(double.PositiveInfinity, actual);
    }

    // ------------------------------------------------------------------ Divide

    [Fact]
    public void Divide_TwoNormalNumbers_ReturnsQuotient()
    {
        double actual = _calculator.Divide(10, 4);

        Assert.Equal(2.5, actual);
    }

    [Fact]
    public void Divide_ZeroByNonZero_ReturnsZero()
    {
        double actual = _calculator.Divide(0, 5);

        Assert.Equal(0, actual);
    }

    [Fact]
    public void Divide_ByZero_ThrowsArgumentException()
    {
        // Arrange / Act
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _calculator.Divide(10, 0));

        // Assert: the exact message matters, it is part of the behaviour.
        Assert.Equal("Division by zero: divisor must not be 0", error.Message);
    }

    [Fact]
    public void Divide_MaxValueByVerySmallNumber_ReturnsPositiveInfinity()
    {
        // Borderline: the result is too large to represent.
        double actual = _calculator.Divide(double.MaxValue, 0.5);

        Assert.Equal(double.PositiveInfinity, actual);
    }
}
