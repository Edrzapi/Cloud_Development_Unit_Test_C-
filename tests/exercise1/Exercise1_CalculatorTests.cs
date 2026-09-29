using Exercises.Exercise1;

namespace Exercises.Tests;

/// <summary>
/// EXERCISE 1: testing existing code.
///
/// The Calculator in src/exercise1/Calculator.cs already exists and already works.
/// This exercise is about writing tests for code you did not write: reading it, deciding
/// what is worth checking, and proving it behaves as documented.
///
/// WHAT YOU DO HERE. Fill in the twelve stubs below with tests for Add, Subtract, Multiply
/// and Divide: at least three per method, one ordinary case and the borderline values at
/// the edges of what a double can hold. You do not edit src/, only this file.
///
/// TWO PARTS, IN THIS ORDER.
///   1. Write the test plan FIRST. Copy tasks/TEST_PLAN_TEMPLATE.md to TEST_PLAN.md at the
///      root of this repository and fill in the exercise 1 table: ID, method, description,
///      inputs, expected output, actual output. Writing the plan is the exercise.
///   2. Then implement it. Each row becomes one test down here. The tests are just the plan
///      turned into C#.
///
/// HOW THE TODOs WORK. Every unwritten test carries an [Ignore("TODO ...")] attribute, so
/// NUnit reports it as SKIPPED rather than failed and a fresh clone is green. To activate
/// one, delete its [Ignore] line, leaving [Test] alone. It will now fail, which is correct.
/// Then replace the comment and the Assert.Fail with arrange, act and assert.
///
/// TO RUN, from the csharp folder at the root of this repository:
///   dotnet build                                                      compile
///   dotnet test                                                       the whole suite
///   dotnet test --filter "FullyQualifiedName~Exercise1"               just this exercise
///   dotnet test --filter "FullyQualifiedName~Add_TwoSmallNumbers"     one test by name
///
/// Done looks like all 13 tests in this class passing and none skipped.
///
/// THE FULL BRIEF, with the guide's own test plan table, is in
/// tasks/01_testing_existing_code.md.
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
        // In JUnit this assertion is assertEquals(expected, actual), with the expected
        // value FIRST. NUnit's constraint form puts the actual value first. Getting the
        // order backwards still compiles and still passes, but names the wrong side when
        // it fails.
    }

    // ---------------------------------------------------------------------------------
    // Add
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Add_TwoNegativeNumbers_ReturnsNegativeSum()
    {
        // Should assert that Add(-10, -30) returns -40: two negatives add to their
        // negative total. This is the ordinary-input row the guide asks for.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Add_MaxValueToMaxValue_ReturnsPositiveInfinity()
    {
        // Borderline row, the top end: should assert that
        // Add(double.MaxValue, double.MaxValue) returns double.PositiveInfinity.
        // A double cannot hold the true answer, so it overflows rather than throwing.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Add_TwoVerySmallNumbers_ReturnsSumOfSmallestValues()
    {
        // Borderline row, the small end: should assert that
        // Add(double.Epsilon, double.Epsilon) returns 2 * double.Epsilon, the smallest
        // positive values a double can represent. If you use double.MinValue instead,
        // note it is the most NEGATIVE double, not the smallest positive one, so
        // MinValue + MinValue underflows to double.NegativeInfinity. Either row is worth
        // having; say in your plan which one you meant.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Subtract
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Subtract_LargerFromSmaller_ReturnsNegativeResult()
    {
        // Should assert that Subtract(10, 30) returns -20: taking a bigger number from a
        // smaller one goes below zero.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Subtract_NumberFromItself_ReturnsZero()
    {
        // Should assert that Subtract(42, 42) returns exactly 0. Any number minus itself
        // is zero, and this one is exact, so it needs no tolerance.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Subtract_MinValueMinusMaxValue_ReturnsNegativeInfinity()
    {
        // Borderline row: should assert that Subtract(double.MinValue, double.MaxValue)
        // returns double.NegativeInfinity, the underflow partner of the Add overflow case.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Multiply
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Multiply_TwoNormalNumbers_ReturnsProduct()
    {
        // Should assert that Multiply(6, 7) returns 42. The ordinary-input row.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Multiply_AnyNumberByZero_ReturnsZero()
    {
        // Should assert that Multiply(42, 0) returns 0, and that it does NOT throw:
        // it is only DIVIDING by zero that this Calculator rejects.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Multiply_MaxValueByTwo_ReturnsPositiveInfinity()
    {
        // Borderline row: should assert that Multiply(double.MaxValue, 2) returns
        // double.PositiveInfinity, because the true product is past the top of the range.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Divide
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Divide_TwoNormalNumbers_ReturnsQuotient()
    {
        // Should assert that Divide(10, 4) returns 2.5. This one is exact in binary
        // floating point; when a row of yours is not, add a tolerance, as the guide shows:
        // Assert.That(actual, Is.EqualTo(0.3).Within(0.0000000001)).
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Divide_ByZero_ThrowsArgumentException()
    {
        // Should assert that Divide(10, 0) throws ArgumentException, and that its Message
        // is exactly "Division by zero: divisor must not be 0". Assert the message as well
        // as the type: the type alone would pass for the wrong reason.
        // Hint: Assert.Throws<ArgumentException>(() => _calculator.Divide(10, 0));
        // returns the exception it caught, so you can go on to check error.Message.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Divide_ZeroByNonZero_ReturnsZero()
    {
        // The other side of the boundary: should assert that Divide(0, 10) returns 0 and
        // does NOT throw. Only a divisor of zero is rejected, not a dividend of zero.
        Assert.Fail("Not implemented yet");
    }
}
