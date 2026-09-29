using Exercises.Exercise1;

namespace Exercises.Tests;

/// <summary>
/// EXERCISE 1: testing existing code.
///
/// Write a test plan first (copy tasks/TEST_PLAN_TEMPLATE.md), then turn each row into a test
/// down here. Aim for at least three cases per method: a normal one, and the borderline
/// values at the edges of what a double can hold.
///
/// Every test below that is still a TODO carries [Ignore], so the suite passes on a fresh
/// clone. Delete the [Ignore] line once you have written the test.
/// </summary>
[TestFixture]
public class Exercise1_CalculatorTests
{
    private Calculator _calculator;

    // [SetUp] runs before EVERY test, exactly like JUnit's @BeforeEach. NUnit reuses one
    // instance of this class for the whole fixture, so building a fresh calculator here is
    // what stops one test affecting another.
    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    // ---------------------------------------------------------------------------------
    // WORKED EXAMPLE. This is row 1 of the test plan in the exercise guide.
    // Copy this shape for the rest. Notice the three labelled steps, and the name:
    // Method_Scenario_ExpectedResult.
    // ---------------------------------------------------------------------------------
    [Test]
    public void Add_TwoSmallNumbers_ReturnsTheirSum()
    {
        // Arrange: set up the inputs and the answer we expect.
        double num1 = 10;
        double num2 = 30;
        double expected = 40;

        // Act: call the one method under test.
        double actual = _calculator.Add(num1, num2);

        // Assert: check what came back.
        Assert.That(actual, Is.EqualTo(expected));
    }

    // ---------------------------------------------------------------------------------
    // Add
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Add_TwoNegativeNumbers_ReturnsNegativeSum()
    {
        // Should assert that adding two negative numbers gives their negative total.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Add_MaxValueToMaxValue_ReturnsPositiveInfinity()
    {
        // Should assert that double.MaxValue + double.MaxValue overflows to double.PositiveInfinity.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Add_TwoVerySmallNumbers_ReturnsSumOfSmallestValues()
    {
        // Should assert what happens at the smallest end, using double.Epsilon or double.MinValue.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Subtract
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Subtract_LargerFromSmaller_ReturnsNegativeResult()
    {
        // Should assert that subtracting a bigger number from a smaller one goes below zero.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Subtract_NumberFromItself_ReturnsZero()
    {
        // Should assert that any number minus itself is exactly zero.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Subtract_MinValueMinusMaxValue_ReturnsNegativeInfinity()
    {
        // Should assert that the borderline case underflows to double.NegativeInfinity.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Multiply
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Multiply_TwoNormalNumbers_ReturnsProduct()
    {
        // Should assert a plain multiplication, for example 6 * 7 == 42.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Multiply_AnyNumberByZero_ReturnsZero()
    {
        // Should assert that multiplying by zero always gives zero.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Multiply_MaxValueByTwo_ReturnsPositiveInfinity()
    {
        // Should assert that the borderline case overflows to double.PositiveInfinity.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Divide
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Divide_TwoNormalNumbers_ReturnsQuotient()
    {
        // Should assert a plain division, for example 10 / 4 == 2.5.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Divide_ByZero_ThrowsArgumentException()
    {
        // Should assert that Divide(x, 0) throws ArgumentException with the
        // message "Division by zero: divisor must not be 0".
        // Hint: Assert.Throws<ArgumentException>(() => _calculator.Divide(10, 0));
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Divide_ZeroByNonZero_ReturnsZero()
    {
        // Should assert that zero divided by anything non-zero is zero, and does NOT throw.
        Assert.Fail("Not implemented yet");
    }
}
