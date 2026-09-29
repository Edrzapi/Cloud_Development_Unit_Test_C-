# Code corrections: what was wrong in the original, and what changed

The classes in `src/` are a translation of the Java original used by this module. That
original carried two genuine bugs. Both have been **corrected here**, and each correction
carries a short comment in the source explaining why the code is written the way it is.

This file is the record of what was wrong, what it was changed to, and what is worth
noticing. It is not a list of live defects: the exercise code now behaves correctly, so
every test you write should assert correct behaviour.

One error remains live, and it is not in the code: see "Still live" at the bottom.

Read this before exercise 2. It will save you a confusing half hour, and the two bugs are
better lessons than anything you will write a test for today.

---

## Correction 1: `UserService.Login` looked users up by password, not by username

**Where:** `src/Exercises/Exercise2/UserService.cs`, in `Login`.

**Was**

```csharp
_users.TryGetValue(trimmedPassword, out string savedPassword);   // wrong key
if (savedPassword == null) throw new InvalidOperationException("Invalid username supplied");

if (!trimmedPassword.Equals(savedPassword)) throw new ArgumentException("Invalid password supplied");

return username;                                                 // untrimmed
```

**Now**

```csharp
// look the user up by their username, then check the password they supplied
// against the one we stored. Keying the map by password would only ever match
// a user whose name happened to equal their own password.
_users.TryGetValue(trimmedUsername, out string savedPassword);
if (savedPassword == null) throw new InvalidOperationException("Invalid username supplied");

if (!trimmedPassword.Equals(savedPassword)) throw new ArgumentException("Invalid password supplied");

// return the trimmed name, the same form Register returns and stores
return trimmedUsername;
```

**Why it mattered.** `Register` stores `username -> password`. `Login` then read the
dictionary using the **password** as the key. For any normally registered user that key
does not exist, so login always failed with "Invalid username supplied". The guide's own
worked example, row 1 of the exercise 2 test plan ("Register a valid user, login
successfully with said valid user", expected `"bobby"`), could not pass. The only way to
log in was to register a user whose username and password were the same string, which made
the wrong key accidentally correct.

There was a second consequence worth pointing out: the `"Invalid password supplied"` branch
was **unreachable**. The lookup by password could only succeed when the stored password
equalled the key, so the very next comparison could never fail. It was dead code. Fixing
the key is what brought that branch to life.

The return value was also changed from `username` to `trimmedUsername`, so `Login` agrees
with `Register`, which trims before storing and returns the trimmed name.

**What you should notice**

- A lookup key is part of the contract between two methods. `Register` and `Login` have to
  agree on it, and nothing in the type system forces them to.
- Dead code can hide behind a bug. Ask of any branch: what input reaches this line?
- Tests that only check "does it throw" would have passed throughout. It is asserting the
  **message and the type** that pins the behaviour down.

**The stubs in `tests/Exercises.Tests/Exercise2_UserServiceTests.cs` that cover it**

| Test | Proves |
| --- | --- |
| `Login_RegisteredUserWithCorrectPassword_ReturnsUsername` | register then login returns `"bobby"` |
| `Login_RegisteredUserWithWrongPassword_ThrowsArgumentException` | the once-unreachable `"Invalid password supplied"` branch now runs |
| `Login_UnknownUsername_ThrowsInvalidOperationException` | an unknown username still gives `"Invalid username supplied"` |

A fourth row is worth adding to your own plan: log in with an untrimmed username such as
`"  bobby  "` and check you get `"bobby"` back, the same form `Register` returns.

---

## Correction 2: the password character rules used a broken regular expression

**Where:** `src/Exercises/Exercise2/UserService.cs` **and**
`src/Exercises/Exercise3/UserController.cs`. Both classes carry the same three rules.

**Was**

```csharp
private const string HasUppercase = @"\A[A-Z|a-z|1-9]*[A-Z]+[A-Z|a-z|1-9]*\z";
private const string HasLowercase = @"\A[A-Z|a-z|1-9]*[a-z]+[A-Z|a-z|1-9]*\z";
private const string HasNumber    = @"\A[A-Z|a-z|1-9]*[1-9]+[A-Z|a-z|1-9]*\z";
```

**Now**

```csharp
// One character class per rule. Each is used as a "contains" test, so it needs no
// anchors and no surrounding .* padding.
private const string HasUppercase = "[A-Z]";
private const string HasLowercase = "[a-z]";
private const string HasNumber = "[0-9]";
```

used as, in this order, with the messages unchanged:

```csharp
// password must contain at least 1 uppercase character. A "contains" check, not a
// whole-string match: a whole-string match fails on any character the class does not
// list, so a single unexpected symbol would report the wrong rule.
if (!Regex.IsMatch(trimmedPassword, HasUppercase)) throw new ArgumentException("Password must contain at least 1 uppercase character");

// password must contain at least 1 lowercase character
if (!Regex.IsMatch(trimmedPassword, HasLowercase)) throw new ArgumentException("Password must contain at least 1 lowercase character");

// password must contain at least 1 number. The class is [0-9], not [1-9]: zero is
// a number too.
if (!Regex.IsMatch(trimmedPassword, HasNumber)) throw new ArgumentException("Password must contain at least 1 number character");
```

**Why it mattered.** Three separate faults in one expression:

1. **The pipes were literal.** Inside `[...]`, `|` is not alternation, it is just the pipe
   character. The class silently allowed `|` in a password. Somebody had read `[A-Z|a-z|1-9]`
   as "uppercase **or** lowercase **or** digit", which is what `[A-Za-z0-9]` already says.
2. **`1-9` excluded zero.** The rule is meant to say "at least 1 number", and `0` is a
   number.
3. **The match was whole-string.** Java's `String.matches()` must match the entire string,
   and the C# port reproduced that with `\A ... \z`. So any character outside the class
   failed **every** rule, not just the one it looked related to. Because the uppercase rule
   is checked first, that is the message the user was given.

Fault 3 is the instructive one. `"Codes0"` is a perfectly reasonable password containing an
uppercase letter, lowercase letters and a digit. It was rejected, and the reason given was
"Password must contain at least 1 uppercase character". The same happened to any password
containing a symbol: `"Codes123!"` was rejected, and blamed on uppercase.

A "contains" check has none of these problems, which is why the correction does not simply
patch the class to `[A-Za-z0-9]`. Padding a needle with `.*` on both sides is a whole-string
match pretending to be a containment test, and it fails the moment the class is incomplete.
`Regex.IsMatch(password, "[A-Z]")` is already a containment test: it searches, it does not
have to consume the whole string, so characters the rule says nothing about are simply
ignored, which is exactly what the three stated rules intend.

**Behaviour before and after**

| Input | Before | After |
| --- | --- | --- |
| `"Codes0"` | rejected: "at least 1 **uppercase** character" | accepted |
| `"Codes123!"` | rejected: "at least 1 **uppercase** character" | accepted |
| `"Cod\|es1"` | accepted, because the pipe was a literal member of the class | accepted, because no rule forbids symbols |
| `"codes1"` | "at least 1 uppercase character" | unchanged |
| `"CODES1"` | "at least 1 lowercase character" | unchanged |
| `"Codesss"` | "at least 1 number character" | unchanged |

The three messages are unchanged, character for character. Only which inputs reach them
has changed.

**What you should notice**

- A regular expression that is doing the wrong **kind** of match will still compile, still
  run, and still return a boolean. Nothing warns you.
- When validation rules are checked in sequence, an input that breaks two rules only ever
  reports the first. An error message is a claim about the input, and a wrong claim sends
  the reader looking in the wrong place.
- `[A-Z|a-z|1-9]` looks deliberate. Reading it aloud as "A to Z, or pipe, or a to z, or
  pipe, or 1 to 9" is what exposes it.
- The interesting test cases are the boundary ones: zero, not "a digit"; a symbol, which no
  rule mentions at all.

**The stubs that cover it.** In `Exercise2_UserServiceTests`:
`Register_PasswordWhoseOnlyDigitIsZero_IsAccepted`,
`Register_PasswordContainingASymbol_IsAccepted`, plus the three unchanged message tests
`Register_PasswordWithNoUppercase_...`, `..._NoLowercase_...`, `..._NoNumber_...`. The same
two boundary cases are worth adding to your exercise 3 plan against `UserController`, where
they also let you verify that the user now reaches the repository.

---

## A faithfully ported oddity that has been left alone

**`UserController.Login` does not trim.** `Register` trims username and password before
validating; `Login` checks `username.Length == 0` on the raw string. A username of `"   "`
therefore passes straight through to the repository.

This is ported as-is from the Java original and has **not** been changed. It is not a bug
in the same sense as the two above: the controller's job in exercise 3 is explicitly to
hand the "is this user real" question to the repository, so where the boundary sits is a
design decision rather than a mistake, and the guide says as much. It is still worth
noticing as an inconsistency between two methods on the same class, and
[`tasks/03_mocking.md`](tasks/03_mocking.md) asks you to write the test that pins it down.

---

## Still live: the exercise guide's own example row uses a password that is too short

**Where:** the exercise guide, exercise 2, test plan row 2. This is a **worksheet** error,
not a code one, so there is nothing in `src/` to correct.

The row says: register `username="bobby"`, `password="Codes"`, expected
`IllegalArgumentException("Password must contain at least 1 number character")`.

`"Codes"` is **five** characters. The length rule is checked before the number rule, so the
code never reaches the number check, and the message is actually
`"Password must contain at least 6 characters"`.

**What this repository does about it:** the worked example in
`tests/Exercises.Tests/Exercise2_UserServiceTests.cs` uses `"Codesss"` (seven characters, no
digit), which is what the guide meant, and carries a comment saying so.
[`tasks/02_testing_exceptions.md`](tasks/02_testing_exceptions.md) reproduces the guide's
table faithfully and adds a footnote underneath explaining what actually happens.

It is a genuine lesson rather than a typo to skip past: the order of validation checks is
part of the behaviour, and a test plan written without reading the code will get it wrong.

---

## Summary

| | What it was | State |
| --- | --- | --- |
| Login lookup keyed by password | Registered users could never log in; the wrong-password branch was dead code | Corrected, commented in source |
| Password character rules | `"Codes0"` and any symbol rejected, and blamed on the uppercase rule | Corrected in both classes, commented in source |
| `UserController.Login` does not trim | `"   "` reaches the repository | Left as-is, deliberately, and worth a test |
| Guide's row 2 uses `"Codes"` | Reports the 6 character rule, not the number rule | Still live, it is in the worksheet |

The skeleton in `tests/` ships green: every unimplemented test carries `[Ignore]` and so is
reported skipped, and `dotnet test` exits 0 on a fresh clone.

```
Passed!  - Failed:     0, Passed:     3, Skipped:    51, Total:    54
```
