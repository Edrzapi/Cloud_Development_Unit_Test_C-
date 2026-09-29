using Exercises.Exercise3;
using Moq;

namespace Exercises.Solutions;

/// <summary>
/// EXERCISE 3 SOLUTION. Trainer copy, all tests implemented and passing.
///
/// The repository is mocked with Moq, so no storage is involved: every test controls
/// exactly what the repository appears to do, and then checks both the value returned
/// and whether the repository was called at all.
/// </summary>
public class Exercise3_UserControllerTests
{
    private readonly Mock<IUserRepository> _repository = new Mock<IUserRepository>();
    private readonly UserController _controller;

    public Exercise3_UserControllerTests()
    {
        _controller = new UserController(_repository.Object);
    }

    // ------------------------------------------------------------------ Register

    [Fact]
    public void Register_ValidUser_StoresUserViaRepository()
    {
        // Arrange
        User input = new User(0, "bobby", "Codes123");
        User saved = new User(1, "bobby", "Codes123");
        _repository.Setup(r => r.Exists("bobby")).Returns(false);
        _repository.Setup(r => r.Register(input)).Returns(saved);

        // Act
        User actual = _controller.Register(input);

        // Assert
        Assert.Equal(saved, actual);
        _repository.Verify(r => r.Exists("bobby"), Times.Once);
        _repository.Verify(r => r.Register(input), Times.Once);
    }

    [Fact]
    public void Register_UsernameIsTrimmedBeforeTheExistsCheck()
    {
        // Arrange: the controller trims before asking the repository.
        User input = new User(0, "  bobby  ", "Codes123");
        _repository.Setup(r => r.Exists("bobby")).Returns(false);
        _repository.Setup(r => r.Register(input)).Returns(input);

        // Act
        _controller.Register(input);

        // Assert: the untrimmed form must never reach the repository.
        _repository.Verify(r => r.Exists("bobby"), Times.Once);
        _repository.Verify(r => r.Exists("  bobby  "), Times.Never);
    }

    [Fact]
    public void Register_NullUser_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(null));

        Assert.Equal("User must not be null", error.Message);
        _repository.Verify(r => r.Register(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void Register_NullUsername_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, null, "Codes123")));

        Assert.Equal("Username must not be null", error.Message);
        _repository.Verify(r => r.Register(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void Register_WhitespaceOnlyUsername_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, "   ", "Codes123")));

        Assert.Equal("Username must not be whitespace only", error.Message);
    }

    [Fact]
    public void Register_NullPassword_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, "bobby", null)));

        Assert.Equal("Password must not be null", error.Message);
    }

    [Fact]
    public void Register_WhitespaceOnlyPassword_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, "bobby", "   ")));

        Assert.Equal("Password must not be whitespace only", error.Message);
    }

    [Fact]
    public void Register_UsernameShorterThanFourCharacters_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, "bob", "Codes123")));

        Assert.Equal("Username must contain at least 4 characters", error.Message);
        // The username never even gets as far as the repository.
        _repository.Verify(r => r.Exists(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void Register_RepositorySaysUsernameExists_ThrowsArgumentException()
    {
        // Arrange: this is the new exception the guide mentions, and it only happens
        // because the mock says so. No database needed.
        _repository.Setup(r => r.Exists("bobby")).Returns(true);

        // Act
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, "bobby", "Codes123")));

        // Assert
        Assert.Equal("Username already exists", error.Message);
        _repository.Verify(r => r.Register(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void Register_PasswordShorterThanSixCharacters_ThrowsArgumentException()
    {
        _repository.Setup(r => r.Exists(It.IsAny<string>())).Returns(false);

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, "bobby", "Cod1")));

        Assert.Equal("Password must contain at least 6 characters", error.Message);
    }

    [Fact]
    public void Register_PasswordWithNoUppercase_ThrowsArgumentException()
    {
        _repository.Setup(r => r.Exists(It.IsAny<string>())).Returns(false);

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, "bobby", "codes123")));

        Assert.Equal("Password must contain at least 1 uppercase character", error.Message);
    }

    [Fact]
    public void Register_PasswordWithNoLowercase_ThrowsArgumentException()
    {
        _repository.Setup(r => r.Exists(It.IsAny<string>())).Returns(false);

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, "bobby", "CODES123")));

        Assert.Equal("Password must contain at least 1 lowercase character", error.Message);
    }

    [Fact]
    public void Register_PasswordWithNoNumber_ThrowsArgumentException()
    {
        _repository.Setup(r => r.Exists(It.IsAny<string>())).Returns(false);

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Register(new User(0, "bobby", "Codesss")));

        Assert.Equal("Password must contain at least 1 number character", error.Message);
        _repository.Verify(r => r.Register(It.IsAny<User>()), Times.Never);
    }

    // The controller carries the same three password rules as UserService, corrected the
    // same way. Zero counts as a number, so this user is valid and reaches the repository.
    // Under the original rules it was rejected, and with the UPPERCASE message.
    // See CODE_CORRECTIONS.md, correction 2.
    [Fact]
    public void Register_PasswordWhoseOnlyDigitIsZero_IsAccepted()
    {
        User input = new User(0, "bobby", "Codes0");
        _repository.Setup(r => r.Exists(It.IsAny<string>())).Returns(false);
        _repository.Setup(r => r.Register(input)).Returns(input);

        User actual = _controller.Register(input);

        Assert.Equal(input, actual);
        _repository.Verify(r => r.Register(input), Times.Once);
    }

    // No stated rule forbids symbols, so a symbol must not reject the password. The
    // original whole-string match rejected it, and blamed the uppercase rule.
    [Fact]
    public void Register_PasswordContainingASymbol_IsAccepted()
    {
        User input = new User(0, "bobby", "Cod|es1");
        _repository.Setup(r => r.Exists(It.IsAny<string>())).Returns(false);
        _repository.Setup(r => r.Register(input)).Returns(input);

        User actual = _controller.Register(input);

        Assert.Equal(input, actual);
        _repository.Verify(r => r.Register(input), Times.Once);
    }

    // ------------------------------------------------------------------ Login

    [Fact]
    public void Login_ValidUser_ReturnsUserFromRepository()
    {
        // Arrange
        User input = new User(0, "bobby", "Codes123");
        User stored = new User(1, "bobby", "Codes123");
        _repository.Setup(r => r.Login(input)).Returns(stored);

        // Act
        User actual = _controller.Login(input);

        // Assert: the controller does not decide anything here, it just passes through.
        Assert.Equal(stored, actual);
        _repository.Verify(r => r.Login(input), Times.Once);
    }

    [Fact]
    public void Login_NullUser_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Login(null));

        Assert.Equal("User must not be null", error.Message);
        _repository.Verify(r => r.Login(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void Login_NullUsername_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Login(new User(0, null, "Codes123")));

        Assert.Equal("Username and password must not be null", error.Message);
    }

    [Fact]
    public void Login_NullPassword_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Login(new User(0, "bobby", null)));

        Assert.Equal("Username and password must not be null", error.Message);
    }

    [Fact]
    public void Login_EmptyUsername_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Login(new User(0, "", "Codes123")));

        Assert.Equal("Username and password must not be empty", error.Message);
        _repository.Verify(r => r.Login(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void Login_EmptyPassword_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _controller.Login(new User(0, "bobby", "")));

        Assert.Equal("Username and password must not be empty", error.Message);
    }

    [Fact]
    public void Login_WhitespaceOnlyUsername_IsPassedStraightToTheRepository()
    {
        // The Login method does NOT trim, unlike Register, so " " is not "empty" to it.
        // Ported faithfully from the Java original. Worth discussing in the debrief:
        // the repository is left to deal with it.
        User input = new User(0, "   ", "Codes123");
        _repository.Setup(r => r.Login(input)).Returns(input);

        User actual = _controller.Login(input);

        Assert.Equal(input, actual);
        _repository.Verify(r => r.Login(input), Times.Once);
    }
}
