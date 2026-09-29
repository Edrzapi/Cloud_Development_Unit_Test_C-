using Exercises.Exercise2;

namespace Exercises.Tests;

/// <summary>
/// EXERCISE 2: testing exceptions.
///
/// Write a test case for every exception Register and Login can throw, then implement them.
///
/// Every test below can pass. If one does not, read the code and work out which rule fired
/// and why: the ORDER the rules are checked in is part of the behaviour, and an input that
/// breaks two rules only ever reports the first.
/// </summary>
public class Exercise2_UserServiceTests
{
    // Fresh service per test, so one test's registered users cannot leak into another.
    private readonly UserService _service = new UserService();

    // ---------------------------------------------------------------------------------
    // WORKED EXAMPLE. This is row 2 of the test plan in the exercise guide: registering
    // with a password that has no number in it.
    // ---------------------------------------------------------------------------------
    [Fact]
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

        // Assert: check we got the RIGHT exception, not just any exception.
        Assert.Equal("Password must contain at least 1 number character", error.Message);
    }

    // ---------------------------------------------------------------------------------
    // Register: the happy path and the validation rules, in the order the code checks them.
    // ---------------------------------------------------------------------------------

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_ValidDetails_ReturnsTrimmedUsername()
    {
        // Should assert that Register("  bobby  ", "Codes123") returns "bobby".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_NullUsername_ThrowsArgumentException()
    {
        // Should assert the message "Username must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_WhitespaceOnlyUsername_ThrowsArgumentException()
    {
        // Should assert the message "Username must not be whitespace only".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_NullPassword_ThrowsArgumentException()
    {
        // Should assert the message "Password must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_WhitespaceOnlyPassword_ThrowsArgumentException()
    {
        // Should assert the message "Password must not be whitespace only".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_UsernameShorterThanFourCharacters_ThrowsArgumentException()
    {
        // Should assert the message "Username must contain at least 4 characters".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_UsernameAlreadyRegistered_ThrowsArgumentException()
    {
        // Should register a user first, then assert the second attempt says
        // "Username already exists".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_PasswordShorterThanSixCharacters_ThrowsArgumentException()
    {
        // Should assert the message "Password must contain at least 6 characters".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_PasswordWithNoUppercase_ThrowsArgumentException()
    {
        // Should assert the message "Password must contain at least 1 uppercase character".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_PasswordWithNoLowercase_ThrowsArgumentException()
    {
        // Should assert the message "Password must contain at least 1 lowercase character".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_PasswordWhoseOnlyDigitIsZero_IsAccepted()
    {
        // Should assert that Register("bobby", "Codes0") returns "bobby", because "Codes0"
        // does contain a number. Zero counts.
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Register_PasswordContainingASymbol_IsAccepted()
    {
        // Should assert that Register("bobby", "Cod|es1") returns "bobby". None of the
        // three character rules forbids a symbol, so a symbol must not reject a password.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Login
    // ---------------------------------------------------------------------------------

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Login_RegisteredUserWithCorrectPassword_ReturnsUsername()
    {
        // Should register "bobby"/"Codes123", then assert Login("bobby", "Codes123")
        // returns "bobby". This is row 1 of the guide's own test plan.
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Login_RegisteredUserWithWrongPassword_ThrowsArgumentException()
    {
        // Should register "bobby"/"Codes123", then assert Login("bobby", "wrong1A") throws
        // ArgumentException with the message "Invalid password supplied".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Login_NullUsername_ThrowsArgumentException()
    {
        // Should assert the message "Username and password must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Login_NullPassword_ThrowsArgumentException()
    {
        // Should assert the message "Username and password must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Login_EmptyUsername_ThrowsArgumentException()
    {
        // Should assert the message "Username and password must not be empty".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Login_EmptyPassword_ThrowsArgumentException()
    {
        // Should assert the message "Username and password must not be empty".
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: implement me, then delete this Skip")]
    public void Login_UnknownUsername_ThrowsInvalidOperationException()
    {
        // Should assert that logging in without registering throws
        // InvalidOperationException with the message "Invalid username supplied".
        Assert.Fail("Not implemented yet");
    }
}
