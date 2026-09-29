namespace Exercises.Tests;

/// <summary>
/// EXERCISE 3, PART 3 (stretch): test-driven development.
///
/// There is no ConcreteUserRepository class yet. That is the point. Work in this order:
///
///   1. Write the test plan for Exists, Register and Login.
///   2. Create ConcreteUserRepository in src/Exercises/Exercise3, implementing
///      IUserRepository, with empty method bodies that throw NotImplementedException.
///      Store the users in a private List&lt;User&gt; field.
///   3. Write ONE test below. Run it. Watch it fail (red).
///   4. Write just enough of the implementation to make it pass (green).
///   5. Tidy up, then go back to step 3 for the next test.
///
/// Add "using Exercises.Exercise3;" at the top once your class exists, and delete each
/// Skip as you write the test it belongs to.
/// </summary>
public class Exercise3_Stretch_ConcreteUserRepositoryTests
{
    [Fact(Skip = "TODO: stretch task. Create ConcreteUserRepository first, then write this.")]
    public void Exists_UsernameNotStored_ReturnsFalse()
    {
        // Should assert that a brand new, empty repository does not know any username.
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: stretch task.")]
    public void Exists_UsernameAlreadyRegistered_ReturnsTrue()
    {
        // Should register a user, then assert Exists returns true for that username.
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: stretch task.")]
    public void Register_NewUser_StoresUserAndReturnsIt()
    {
        // Should assert the returned user matches what went in, and that Exists now finds
        // it. Decide who assigns the id, and write a test that pins that decision down.
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: stretch task.")]
    public void Login_MatchingUsernameAndPassword_ReturnsStoredUser()
    {
        // Should register a user, then assert logging in with the same details returns
        // the stored user, including the id the repository gave it.
        Assert.Fail("Not implemented yet");
    }

    [Fact(Skip = "TODO: stretch task.")]
    public void Login_WrongPassword_DoesNotReturnAUser()
    {
        // Should assert what your design does on a bad password: throw, or return null.
        // Whichever you choose, the test is what makes it a decision rather than an accident.
        Assert.Fail("Not implemented yet");
    }
}
