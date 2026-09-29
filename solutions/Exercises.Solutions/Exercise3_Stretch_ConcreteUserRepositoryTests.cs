using Exercises.Exercise3;

namespace Exercises.Solutions;

/// <summary>
/// STRETCH TASK SOLUTION. These are the tests that drove ConcreteUserRepository into
/// existence, written one at a time, each one failing before the code was written.
///
/// No mocks here: the repository IS the thing under test, and its List of users is its
/// own business, so we test it through its public methods only.
/// </summary>
public class Exercise3_Stretch_ConcreteUserRepositoryTests
{
    private readonly ConcreteUserRepository _repository = new ConcreteUserRepository();

    // ------------------------------------------------------------------ Exists

    [Fact]
    public void Exists_UsernameNotStored_ReturnsFalse()
    {
        // Arrange: a brand new repository knows nobody.
        bool actual = _repository.Exists("bobby");

        Assert.False(actual);
    }

    [Fact]
    public void Exists_UsernameAlreadyRegistered_ReturnsTrue()
    {
        // Arrange
        _repository.Register(new User(0, "bobby", "Codes123"));

        // Act
        bool actual = _repository.Exists("bobby");

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void Exists_DifferentUsername_ReturnsFalse()
    {
        _repository.Register(new User(0, "bobby", "Codes123"));

        Assert.False(_repository.Exists("alice"));
    }

    // ------------------------------------------------------------------ Register

    [Fact]
    public void Register_NewUser_ReturnsStoredUserWithAnId()
    {
        // Arrange
        User input = new User(0, "bobby", "Codes123");

        // Act
        User actual = _repository.Register(input);

        // Assert: the repository, not the caller, decides the id.
        Assert.Equal(1, actual.Id);
        Assert.Equal("bobby", actual.Username);
        Assert.Equal("Codes123", actual.Password);
    }

    [Fact]
    public void Register_SecondUser_GetsTheNextId()
    {
        _repository.Register(new User(0, "bobby", "Codes123"));

        User actual = _repository.Register(new User(0, "alice", "Codes456"));

        Assert.Equal(2, actual.Id);
    }

    [Fact]
    public void Register_NewUser_MakesExistsReturnTrue()
    {
        _repository.Register(new User(0, "bobby", "Codes123"));

        Assert.True(_repository.Exists("bobby"));
    }

    [Fact]
    public void Register_DuplicateUsername_ThrowsArgumentException()
    {
        _repository.Register(new User(0, "bobby", "Codes123"));

        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _repository.Register(new User(0, "bobby", "Codes456")));

        Assert.Equal("Username already exists", error.Message);
    }

    [Fact]
    public void Register_NullUser_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _repository.Register(null));

        Assert.Equal("User must not be null", error.Message);
    }

    [Fact]
    public void Register_StoresACopy_SoLaterChangesToTheCallerObjectDoNotLeakIn()
    {
        // Arrange
        User input = new User(0, "bobby", "Codes123");
        _repository.Register(input);

        // Act: the caller changes its own object after registering.
        input.Password = "Hacked99";

        // Assert: the stored password is untouched, so logging in still needs the real one.
        User found = _repository.Login(new User(0, "bobby", "Codes123"));
        Assert.Equal("Codes123", found.Password);
    }

    // ------------------------------------------------------------------ Login

    [Fact]
    public void Login_MatchingUsernameAndPassword_ReturnsStoredUser()
    {
        // Arrange
        User stored = _repository.Register(new User(0, "bobby", "Codes123"));

        // Act
        User actual = _repository.Login(new User(0, "bobby", "Codes123"));

        // Assert: including the id the repository allocated.
        Assert.Equal(stored, actual);
    }

    [Fact]
    public void Login_WrongPassword_ThrowsInvalidOperationException()
    {
        _repository.Register(new User(0, "bobby", "Codes123"));

        InvalidOperationException error = Assert.Throws<InvalidOperationException>(
            () => _repository.Login(new User(0, "bobby", "Wrong123")));

        Assert.Equal("Invalid username or password supplied", error.Message);
    }

    [Fact]
    public void Login_UnknownUsername_ThrowsInvalidOperationException()
    {
        InvalidOperationException error = Assert.Throws<InvalidOperationException>(
            () => _repository.Login(new User(0, "nobody", "Codes123")));

        Assert.Equal("Invalid username or password supplied", error.Message);
    }

    [Fact]
    public void Login_NullUser_ThrowsArgumentException()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(
            () => _repository.Login(null));

        Assert.Equal("User must not be null", error.Message);
    }

    // ------------------------------------------------------------------ Wired together

    [Fact]
    public void Controller_WithTheRealRepository_RegistersThenLogsIn()
    {
        // This is no longer a unit test, it is a small integration test: two real classes,
        // no mocks. Worth showing the group the difference.
        UserController controller = new UserController(_repository);

        User registered = controller.Register(new User(0, "bobby", "Codes123"));
        User loggedIn = controller.Login(new User(0, "bobby", "Codes123"));

        Assert.Equal(registered, loggedIn);
    }
}
