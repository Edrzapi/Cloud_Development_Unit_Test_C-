using Exercises.Exercise3;

namespace Exercises.Solutions;

/// <summary>
/// STRETCH TASK SOLUTION: a real implementation of <see cref="IUserRepository"/>, stored
/// in memory in a List of users, built test first.
///
/// "Concrete" in the name simply signals that this is a class, not an interface.
///
/// It lives in the solutions project rather than in src/, because the students are meant
/// to write it themselves. When they do, it belongs in src/Exercises/Exercise3.
/// </summary>
public class ConcreteUserRepository : IUserRepository
{
    // The whole "database", as required by the exercise.
    private readonly List<User> _users = new List<User>();

    // Ids start at 1, so 0 can mean "not saved yet".
    private int _nextId = 1;

    /// <summary>True if a user with that username has already been registered.</summary>
    public bool Exists(string trimmedUsername)
    {
        return _users.Any(u => u.Username == trimmedUsername);
    }

    /// <summary>
    /// Stores a copy of the user with a freshly allocated id, and returns the stored user.
    /// </summary>
    public User Register(User user)
    {
        if (user == null) throw new ArgumentException("User must not be null");
        if (Exists(user.Username)) throw new ArgumentException("Username already exists");

        // Store a copy so a later change to the caller's object cannot alter the database.
        User stored = new User(_nextId, user.Username, user.Password);
        _nextId++;
        _users.Add(stored);

        return stored;
    }

    /// <summary>
    /// Returns the stored user whose username and password both match.
    /// Throws when there is no such user. We chose an exception over returning null so
    /// that a caller cannot ignore a failed login by accident.
    /// </summary>
    public User Login(User user)
    {
        if (user == null) throw new ArgumentException("User must not be null");

        User found = _users.FirstOrDefault(
            u => u.Username == user.Username && u.Password == user.Password);

        if (found == null) throw new InvalidOperationException("Invalid username or password supplied");

        return found;
    }
}
