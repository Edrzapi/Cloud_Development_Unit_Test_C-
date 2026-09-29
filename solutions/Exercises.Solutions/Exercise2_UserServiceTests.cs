using Exercises.Exercise2;

namespace Exercises.Solutions;

/// <summary>
/// EXERCISE 2 SOLUTION. Trainer copy, all tests implemented and passing.
///
/// The Java original carried two genuine bugs, in the login lookup and in the password
/// character rules. Both are corrected in this repository, and the tests below assert the
/// corrected behaviour. CODE_CORRECTIONS.md records what was wrong and what changed, which
/// is the material for the debrief.
/// </summary>
public class Exercise2_UserServiceTests
{
    private readonly UserService _service = new UserService();

    // ------------------------------------------------------------------ Register, happy path

    [Fact]
    public void Register_ValidDetails_ReturnsTrimmedUsername()
    {
        // Arrange / Act
        string actual = _service.Register("  bobby  ", "  Codes123  ");

        // Assert: the service trims before storing and returns the trimmed name.
        Assert.Equal("bobby", actual);
    }

    // ------------------------------------------------------------------ Register, exceptions
    // These are in the order the code checks them, which is also the order you must respect
    // when choosing inputs: an input that breaks two rules only reports the first.

    [Fact]
    public void Register_NullUsername_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register(null, "Codes123"));

        Assert.Equal("Username must not be null", error.Message);
    }

    [Fact]
    public void Register_WhitespaceOnlyUsername_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register("   ", "Codes123"));

        Assert.Equal("Username must not be whitespace only", error.Message);
    }

    [Fact]
    public void Register_NullPassword_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register("bobby", null));

        Assert.Equal("Password must not be null", error.Message);
    }

    [Fact]
    public void Register_WhitespaceOnlyPassword_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register("bobby", "   "));

        Assert.Equal("Password must not be whitespace only", error.Message);
    }

    [Fact]
    public void Register_UsernameShorterThanFourCharacters_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register("bob", "Codes123"));

        Assert.Equal("Username must contain at least 4 characters", error.Message);
    }

    [Fact]
    public void Register_UsernameExactlyFourCharacters_IsAccepted()
    {
        // Borderline: 4 is allowed, 3 is not.
        string actual = _service.Register("bobb", "Codes123");

        Assert.Equal("bobb", actual);
    }

    [Fact]
    public void Register_UsernameAlreadyRegistered_ThrowsArgumentException()
    {
        // Arrange: register once so the name is taken.
        _service.Register("bobby", "Codes123");

        // Act
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register("bobby", "Codes456"));

        // Assert
        Assert.Equal("Username already exists", error.Message);
    }

    [Fact]
    public void Register_PasswordShorterThanSixCharacters_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register("bobby", "Cod1"));

        Assert.Equal("Password must contain at least 6 characters", error.Message);
    }

    [Fact]
    public void Register_PasswordExactlySixCharacters_IsAccepted()
    {
        // Borderline: 6 is allowed.
        string actual = _service.Register("bobby", "Code12");

        Assert.Equal("bobby", actual);
    }

    [Fact]
    public void Register_PasswordWithNoUppercase_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register("bobby", "codes1"));

        Assert.Equal("Password must contain at least 1 uppercase character", error.Message);
    }

    [Fact]
    public void Register_PasswordWithNoLowercase_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register("bobby", "CODES1"));

        Assert.Equal("Password must contain at least 1 lowercase character", error.Message);
    }

    [Fact]
    public void Register_PasswordWithNoNumber_ThrowsArgumentException()
    {
        // The guide's own row uses "Codes", which is 5 characters and so trips the length
        // rule first. See CODE_CORRECTIONS.md, the still-live guide error. "Codesss" is
        // what the guide meant to test.
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Register("bobby", "Codesss"));

        Assert.Equal("Password must contain at least 1 number character", error.Message);
    }

    [Fact]
    public void Register_PasswordWithASymbol_IsAccepted()
    {
        // No stated rule forbids symbols, so a symbol must not reject the password.
        // Under the original whole-string match this threw "at least 1 uppercase
        // character", which was both a rejection and the wrong reason. See
        // CODE_CORRECTIONS.md, correction 2.
        string actual = _service.Register("bobby", "Codes123!");

        Assert.Equal("bobby", actual);
    }

    // The number rule reads [0-9], not the original [1-9], so zero counts as a number.
    // Under the original rules "Codes0" was rejected, and rejected with the UPPERCASE
    // message, because the whole-string match could not consume the 0 either.
    // See CODE_CORRECTIONS.md, correction 2.
    [Fact]
    public void Register_PasswordWhoseOnlyDigitIsZero_IsAccepted()
    {
        string actual = _service.Register("bobby", "Codes0");

        Assert.Equal("bobby", actual);
    }

    // A pipe is just another symbol. It was accepted before the correction too, but for
    // the wrong reason: the pipes inside [A-Z|a-z|1-9] were literal members of the class,
    // not alternation. Now it is accepted because no rule forbids symbols.
    [Fact]
    public void Register_PasswordContainingAPipeCharacter_IsAccepted()
    {
        string actual = _service.Register("bobby", "Cod|es1");

        Assert.Equal("bobby", actual);
    }

    // ------------------------------------------------------------------ Login

    [Fact]
    public void Login_NullUsername_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login(null, "Codes123"));

        Assert.Equal("Username and password must not be null", error.Message);
    }

    [Fact]
    public void Login_NullPassword_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login("bobby", null));

        Assert.Equal("Username and password must not be null", error.Message);
    }

    [Fact]
    public void Login_WhitespaceOnlyUsername_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login("   ", "Codes123"));

        Assert.Equal("Username and password must not be empty", error.Message);
    }

    [Fact]
    public void Login_WhitespaceOnlyPassword_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login("bobby", "   "));

        Assert.Equal("Username and password must not be empty", error.Message);
    }

    [Fact]
    public void Login_UnknownUsername_ThrowsInvalidOperationException()
    {
        // Nobody is registered, so nothing can be found.
        InvalidOperationException error = Assert.Throws<InvalidOperationException>(
            () => _service.Login("bobby", "Codes123"));

        Assert.Equal("Invalid username supplied", error.Message);
    }

    // This is row 1 of the guide's own test plan. Under the original code it could not
    // pass, because Login looked the dictionary up by PASSWORD instead of by username.
    // See CODE_CORRECTIONS.md, correction 1.
    [Fact]
    public void Login_RegisteredUserWithCorrectPassword_ReturnsUsername()
    {
        // Arrange
        _service.Register("bobby", "Codes123");

        // Act
        string actual = _service.Login("bobby", "Codes123");

        // Assert
        Assert.Equal("bobby", actual);
    }

    // The "Invalid password supplied" branch is only reachable once the lookup key is the
    // username: before the correction the lookup failed first and this line was dead code.
    [Fact]
    public void Login_RegisteredUserWithWrongPassword_ThrowsArgumentException()
    {
        // Arrange
        _service.Register("bobby", "Codes123");

        // Act
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _service.Login("bobby", "wrong1A"));

        // Assert
        Assert.Equal("Invalid password supplied", error.Message);
    }

    [Fact]
    public void Login_UsernameNotRegistered_ThrowsInvalidOperationException()
    {
        // Arrange: somebody is registered, just not the one we ask for, so this proves
        // the lookup misses rather than the dictionary simply being empty.
        _service.Register("bobby", "Codes123");

        // Act
        InvalidOperationException error = Assert.Throws<InvalidOperationException>(
            () => _service.Login("nobody", "Codes123"));

        // Assert
        Assert.Equal("Invalid username supplied", error.Message);
    }

    [Fact]
    public void Login_UntrimmedUsername_ReturnsTrimmedUsername()
    {
        // Login trims before looking up, and returns the trimmed name, so it agrees with
        // what Register returned and stored.
        _service.Register("bobby", "Codes123");

        string actual = _service.Login("  bobby  ", "  Codes123  ");

        Assert.Equal("bobby", actual);
    }
}
