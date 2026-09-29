using Exercises.Exercise2;

namespace Exercises.Tests;

/// <summary>
/// EXERCISE 2: testing exceptions.
///
/// UserService, in src/exercise2/UserService.cs, validates a registration and a
/// login and rejects bad input by throwing. This exercise is about proving each rejection
/// happens for the RIGHT reason: most of these throws are the same ArgumentException type,
/// and only the message tells them apart.
///
/// WHAT YOU DO HERE. Read UserService.cs from the top of each method downwards, write a
/// case for every throw you find, and fill in the nineteen stubs below. Assert the type AND
/// the message. Two of the throws are InvalidOperationException, not ArgumentException.
///
/// TWO PARTS, IN THIS ORDER.
///   1. Write the test plan FIRST. Copy tasks/TEST_PLAN_TEMPLATE.md to TEST_PLAN.md at the
///      root of this repository and fill in the exercise 2 table, one row per exception.
///      Writing the plan is the exercise.
///   2. Then implement it. Each row becomes one test down here.
///
/// The ORDER the rules are checked in is part of the behaviour. An input that breaks two
/// rules only ever reports the first, so choose inputs that break exactly the one rule you
/// are testing. Register checks, in order: username null, username whitespace only,
/// password null, password whitespace only, username shorter than 4, username already
/// registered, password shorter than 6, no uppercase, no lowercase, no digit.
///
/// HOW THE TODOs WORK. Every unwritten test carries an [Ignore("TODO ...")] attribute, so
/// NUnit reports it as SKIPPED rather than failed and a fresh clone is green. To activate
/// one, delete its [Ignore] line, leaving [Test] alone. It will now fail, which is correct.
/// Then replace the comment and the Assert.Fail with arrange, act and assert.
///
/// TO RUN, from the csharp folder at the root of this repository:
///   dotnet build                                                        compile
///   dotnet test                                                         the whole suite
///   dotnet test --filter "FullyQualifiedName~Exercise2"                 just this exercise
///   dotnet test --filter "FullyQualifiedName~Register_NullUsername"     one test by name
///
/// Done looks like all 20 tests in this class passing and none skipped.
///
/// THE FULL BRIEF, with the guide's own test plan table and the footnote on row 2, is in
/// tasks/02_testing_exceptions.md. The two bugs that were corrected in this class are
/// written up in CODE_CORRECTIONS.md at the root, and both are worth reading before you
/// start.
/// </summary>
[TestFixture]
public class Exercise2_UserServiceTests
{
    private UserService _service;

    // [SetUp] runs before every test, like JUnit's @BeforeEach. A fresh service each time,
    // so one test's registered users cannot leak into another. NUnit reuses a single
    // instance of this class for the whole fixture, which is why the field has to be
    // rebuilt here rather than initialised once where it is declared.
    [SetUp]
    public void SetUp()
    {
        _service = new UserService();
    }

    // ---------------------------------------------------------------------------------
    // WORKED EXAMPLE. This is row 2 of the test plan in the exercise guide: registering
    // with a password that has no number in it.
    // ---------------------------------------------------------------------------------
    [Test]
    public void Register_PasswordWithNoNumber_ThrowsArgumentException()
    {
        // Arrange
        string username = "bobby";
        // The guide's own example uses "Codes", but that is only 5 characters, so the
        // "at least 6 characters" rule fires first and you never reach the number rule.
        // That is an error in the worksheet, not in the code: see tasks/02, the footnote
        // under the test plan table. We use a 7 character password with no digit, which
        // is what the guide meant to test.
        string password = "Codesss";

        // Act: wrap the call in a lambda so the assertion can catch what it throws.
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(username, password));
        // In JUnit this is
        // assertThrows(IllegalArgumentException.class, () -> service.register(...)).
        // Both forms return the caught exception so you can assert on its message; C#
        // passes the type as a generic parameter and writes the lambda with => not ->.

        // Assert: check we got the RIGHT exception, not just any exception.
        Assert.That(error.Message, Is.EqualTo("Password must contain at least 1 number character"));
    }

    // ---------------------------------------------------------------------------------
    // Register: the happy path and the validation rules, in the order the code checks them.
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_ValidDetails_ReturnsTrimmedUsername()
    {
        // Should assert that Register("  bobby  ", "Codes123") returns "bobby". Register
        // trims before it validates and before it stores, and it returns the trimmed name,
        // so the surrounding spaces must not come back.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_NullUsername_ThrowsArgumentException()
    {
        // Should assert that Register(null, "Codes123") throws ArgumentException with the
        // message "Username must not be null". This is the first rule checked.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_WhitespaceOnlyUsername_ThrowsArgumentException()
    {
        // Should assert that Register("   ", "Codes123") throws ArgumentException with the
        // message "Username must not be whitespace only". Note it is not the "at least 4
        // characters" rule that fires: the whitespace rule is checked first.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_NullPassword_ThrowsArgumentException()
    {
        // Should assert that Register("bobby", null) throws ArgumentException with the
        // message "Password must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_WhitespaceOnlyPassword_ThrowsArgumentException()
    {
        // Should assert that Register("bobby", "      ") throws ArgumentException with the
        // message "Password must not be whitespace only", not the length or character
        // rules, which are all checked later.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_UsernameShorterThanFourCharacters_ThrowsArgumentException()
    {
        // Should assert that Register("bob", "Codes123") throws ArgumentException with the
        // message "Username must contain at least 4 characters". This is a boundary, so
        // the guide asks for both sides of it: 3 characters is rejected here, and a
        // 4 character name such as "bobs" is accepted, which is worth a row of its own.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_UsernameAlreadyRegistered_ThrowsArgumentException()
    {
        // Should register "bobby"/"Codes123" successfully first, then assert that a second
        // Register("bobby", "Codes456") throws ArgumentException with the message
        // "Username already exists". Two calls in one test: the first is arrange, the
        // second is the act.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordShorterThanSixCharacters_ThrowsArgumentException()
    {
        // Should assert that Register("bobby", "Cod1") throws ArgumentException with the
        // message "Password must contain at least 6 characters". The other boundary row:
        // 5 characters is rejected, 6 such as "Code12" gets past this rule.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordWithNoUppercase_ThrowsArgumentException()
    {
        // Should assert that Register("bobby", "codes123") throws ArgumentException with
        // the message "Password must contain at least 1 uppercase character". Long enough
        // to clear the length rule, so this is the first rule it breaks.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordWithNoLowercase_ThrowsArgumentException()
    {
        // Should assert that Register("bobby", "CODES123") throws ArgumentException with
        // the message "Password must contain at least 1 lowercase character".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordWhoseOnlyDigitIsZero_IsAccepted()
    {
        // Should assert that Register("bobby", "Codes0") returns "bobby". Zero is a digit,
        // so this password satisfies all three character rules and must NOT throw. This is
        // one of the two boundary cases behind correction 2 in CODE_CORRECTIONS.md: the
        // original rule used [1-9] and rejected it.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordContainingASymbol_IsAccepted()
    {
        // Should assert that Register("bobby", "Cod|es1") returns "bobby". None of the
        // three character rules forbids a symbol, so a symbol must not reject a password.
        // The original whole-string match rejected it and blamed the uppercase rule.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Login
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_RegisteredUserWithCorrectPassword_ReturnsUsername()
    {
        // Should register "bobby"/"Codes123", then assert Login("bobby", "Codes123")
        // returns "bobby". This is row 1 of the guide's own test plan, and the case that
        // could never pass before correction 1. Login trims too, so logging in as
        // "  bobby  " returning "bobby" is worth a second row.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_RegisteredUserWithWrongPassword_ThrowsArgumentException()
    {
        // Should register "bobby"/"Codes123", then assert Login("bobby", "Wrong123")
        // throws ArgumentException with the message "Invalid password supplied". Note the
        // username is known and only the password is wrong, which is what separates this
        // message from "Invalid username supplied".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_NullUsername_ThrowsArgumentException()
    {
        // Should assert that Login(null, "Codes123") throws ArgumentException with the
        // message "Username and password must not be null". Login reports both fields in
        // one message, unlike Register.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_NullPassword_ThrowsArgumentException()
    {
        // Should assert that Login("bobby", null) throws ArgumentException with the same
        // message, "Username and password must not be null". Both rows are worth having:
        // they prove the one check really covers both arguments.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_EmptyUsername_ThrowsArgumentException()
    {
        // Should assert that Login("", "Codes123") throws ArgumentException with the
        // message "Username and password must not be empty".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_EmptyPassword_ThrowsArgumentException()
    {
        // Should assert that Login("bobby", "") throws ArgumentException with the same
        // message, "Username and password must not be empty".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_UnknownUsername_ThrowsInvalidOperationException()
    {
        // Should assert that Login("nobody", "Codes123") on a service where nobody has
        // registered throws InvalidOperationException, NOT ArgumentException, with the
        // message "Invalid username supplied". This is the Java original's bare
        // RuntimeException, and getting the type right is the point of the row.
        Assert.Fail("Not implemented yet");
    }
}
