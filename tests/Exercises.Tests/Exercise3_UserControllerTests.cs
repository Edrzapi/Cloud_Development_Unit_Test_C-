using Exercises.Exercise3;
using Moq;

namespace Exercises.Tests;

/// <summary>
/// EXERCISE 3: mocking in a unit test.
///
/// UserController depends on IUserRepository. We do not want a real database in a unit
/// test, so we hand the controller a MOCK repository: a stand-in we fully control.
///
/// The Java version of this exercise uses Mockito with @Mock and @InjectMocks.
/// In .NET there are no such annotations: you create the mock yourself and pass it to the
/// constructor. That is the "injection" part, done by hand, and it is clearer for it.
///
/// Moq cheat sheet:
///   new Mock&lt;IUserRepository&gt;()                               create the mock
///   mock.Object                                                the IUserRepository to pass in
///   mock.Setup(r =&gt; r.Exists("bobby")).Returns(true)           make a call return something
///   mock.Verify(r =&gt; r.Register(user), Times.Once)             assert a call happened
///   mock.Verify(r =&gt; r.Register(It.IsAny&lt;User&gt;()), Times.Never) assert it did not
/// </summary>
[TestFixture]
public class Exercise3_UserControllerTests
{
    private Mock<IUserRepository> _repository;
    private UserController _controller;

    // A fresh mock and controller for each test. [SetUp] is NUnit's @BeforeEach.
    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<IUserRepository>();

        // Constructor injection: this is where the mock replaces the real repository.
        _controller = new UserController(_repository.Object);
    }

    // ---------------------------------------------------------------------------------
    // WORKED EXAMPLE. Registering a valid user: the controller should ask the repository
    // whether the username exists, and then store the user.
    // ---------------------------------------------------------------------------------
    [Test]
    public void Register_ValidUser_StoresUserViaRepository()
    {
        // Arrange
        User input = new User(0, "bobby", "Codes123");
        User saved = new User(1, "bobby", "Codes123");

        // Tell the mock how to behave: this username is not taken, and saving returns
        // the stored user complete with its new id.
        _repository.Setup(r => r.Exists("bobby")).Returns(false);
        _repository.Setup(r => r.Register(input)).Returns(saved);

        // Act
        User actual = _controller.Register(input);

        // Assert: the right value came back ...
        Assert.That(actual, Is.EqualTo(saved));
        // ... and the controller really did talk to the repository, exactly once.
        _repository.Verify(r => r.Exists("bobby"), Times.Once);
        _repository.Verify(r => r.Register(input), Times.Once);
    }

    // ---------------------------------------------------------------------------------
    // Register
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_NullUser_ThrowsArgumentException()
    {
        // Should assert the message "User must not be null", and that the repository was
        // never touched: _repository.Verify(r => r.Register(It.IsAny<User>()), Times.Never).
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_NullUsername_ThrowsArgumentException()
    {
        // Should assert the message "Username must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_WhitespaceOnlyUsername_ThrowsArgumentException()
    {
        // Should assert the message "Username must not be whitespace only".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_NullPassword_ThrowsArgumentException()
    {
        // Should assert the message "Password must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_WhitespaceOnlyPassword_ThrowsArgumentException()
    {
        // Should assert the message "Password must not be whitespace only".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_UsernameShorterThanFourCharacters_ThrowsArgumentException()
    {
        // Should assert the message "Username must contain at least 4 characters".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_RepositorySaysUsernameExists_ThrowsArgumentException()
    {
        // Should set up the mock so Exists returns TRUE, then assert the message
        // "Username already exists". This is the new exception the guide mentions.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordShorterThanSixCharacters_ThrowsArgumentException()
    {
        // Should assert the message "Password must contain at least 6 characters".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordWithNoUppercase_ThrowsArgumentException()
    {
        // Should assert the message "Password must contain at least 1 uppercase character".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordWithNoLowercase_ThrowsArgumentException()
    {
        // Should assert the message "Password must contain at least 1 lowercase character".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Register_PasswordWithNoNumber_ThrowsArgumentException()
    {
        // Should assert the message "Password must contain at least 1 number character",
        // and that Register was never called on the repository.
        Assert.Fail("Not implemented yet");
    }

    // ---------------------------------------------------------------------------------
    // Login. Note the guide's point: the controller no longer decides whether the user is
    // real, the repository does. So there is far less to check here.
    // ---------------------------------------------------------------------------------

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_ValidUser_ReturnsUserFromRepository()
    {
        // Should set up the mock so Login returns a user, then assert the controller
        // passes it straight back and called the repository once.
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_NullUser_ThrowsArgumentException()
    {
        // Should assert the message "User must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_NullUsernameOrPassword_ThrowsArgumentException()
    {
        // Should assert the message "Username and password must not be null".
        Assert.Fail("Not implemented yet");
    }

    [Test]
    [Ignore("TODO - implement me, then delete this [Ignore] line")]
    public void Login_EmptyUsernameOrPassword_ThrowsArgumentException()
    {
        // Should assert the message "Username and password must not be empty",
        // and that the repository was never asked to log anyone in.
        Assert.Fail("Not implemented yet");
    }
}
