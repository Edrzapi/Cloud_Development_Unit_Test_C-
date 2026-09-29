# Unit testing exercises, C# edition

SDL3 Module 5: Testing. This is the C# version of the module's unit testing exercises, a
translation of the Java original. The three exercises are the same three exercises
described in the module's exercise guide.

**The exercise guide is in this repository: start at [`tasks/README.md`](tasks/README.md).**
You do not need the PDF, and you do not need any other repository. Everything is here.

You do not need to know Java to do these.

The Java original carried two genuine bugs, in the login lookup and in the password
character rules. Both have been corrected here, and the source comments explain why each
piece of code is written the way it is. Trainers: see
[`CODE_CORRECTIONS.md`](CODE_CORRECTIONS.md) for what was wrong, what changed and why.

---

## Prerequisites

- **.NET SDK 9.0** or newer. Check with `dotnet --list-sdks`.
  A `global.json` in this folder pins the build to the 9.0 SDK band, so everyone in the
  room gets the same behaviour even on a machine that also has .NET 10 installed.
- An editor. Visual Studio, Visual Studio Code with the C# Dev Kit extension, or JetBrains
  Rider all work. So does Notepad and a terminal.
- The first build needs internet access, to download the test packages from nuget.org.
  After that it works offline.

## How to run

From this `csharp` folder:

```bash
dotnet build      # compiles the exercise code and your tests
dotnet test       # runs your tests
```

On a fresh clone `dotnet test` **passes**, because every test you have not written yet is
marked as skipped. You will see something like:

```
Passed!  - Failed:     0, Passed:     3, Skipped:    51, Total:    54
```

Those 3 passing tests are the worked examples, one per exercise. The 51 skipped ones are
your job.

To run one exercise at a time:

```bash
dotnet test --filter "FullyQualifiedName~Exercise1"
```

## What is in here

```
csharp/
  UnitTestExercises.sln          the solution; contains src and tests only
  global.json                    pins the SDK to the 9.0 band
  README.md                      this file
  CODE_CORRECTIONS.md            TRAINER: what was wrong in the Java original, and what changed
  tasks/                         THE EXERCISE GUIDE. Start here.
    README.md                    contents page, order of work, how to run
    01_testing_existing_code.md  exercise 1, Calculator
    02_testing_exceptions.md     exercise 2, UserService
    03_mocking.md                exercise 3, UserController with the repository mocked
    04_stretch_tdd_repository.md the stretch task, TDD a ConcreteUserRepository
    TEST_PLAN_TEMPLATE.md        the test plan tables to fill in first
  src/Exercises/                 the code under test. Do not change it.
    Exercise1/Calculator.cs
    Exercise2/UserService.cs
    Exercise3/User.cs
    Exercise3/IUserRepository.cs
    Exercise3/UserController.cs
  tests/Exercises.Tests/         YOUR WORK GOES HERE
    Exercise1_CalculatorTests.cs
    Exercise2_UserServiceTests.cs
    Exercise3_UserControllerTests.cs
    Exercise3_Stretch_ConcreteUserRepositoryTests.cs
  solutions/Exercises.Solutions/ TRAINER: the complete answers
```

`src/Exercises` is the code you are testing. Leave it alone, except in the stretch task,
where you add one new class to `src/Exercises/Exercise3`.

---

## The exercises

Each one is written out in full in [`tasks/`](tasks/README.md), with the guide's own test
plan tables and worked examples. What follows is the short version.

### Exercise 1: testing existing code

`Calculator` has four methods: `Add`, `Subtract`, `Multiply` and `Divide`.

1. **Write a test plan first.** Copy `tasks/TEST_PLAN_TEMPLATE.md` to `TEST_PLAN.md` and
   fill in the exercise 1 table. At least three cases per method. Cover the borderline values
   (what is the largest pair of doubles you can add? the smallest?) as well as ordinary
   ones.
2. **Then implement it**, in `tests/Exercises.Tests/Exercise1_CalculatorTests.cs`. One
   worked example is already there; copy its shape.

Useful for the borderline rows: `double.MaxValue`, `double.MinValue`, `double.Epsilon`,
`double.PositiveInfinity`, `double.NegativeInfinity`.

### Exercise 2: testing exceptions

`UserService.Register` and `UserService.Login` throw on bad input. Plan and then write a
test for **every exception either method can throw**.

The assertion you want is:

```csharp
ArgumentException error = Assert.Throws<ArgumentException>(
    () => _service.Register("bob", "Codes123"));

Assert.Equal("Username must contain at least 4 characters", error.Message);
```

`Assert.Throws<T>` checks the exception type. Checking the **message** as well is what
stops a test passing for the wrong reason: several different rules all throw
`ArgumentException`, and only the message tells them apart.

Watch the **order** the rules are checked in. If an input breaks two rules, you only ever
see the first one.

> One warning about the worksheet: the guide's own example row expects `password="Codes"`
> to report the "at least 1 number" rule. It does not. `"Codes"` is five characters, so the
> length rule is checked first. See the footnote in
> [`tasks/02_testing_exceptions.md`](tasks/02_testing_exceptions.md). Every test in the
> skeleton can pass.

### Exercise 3: mocking

`UserController` does the same validation, but hands the actual storage to an
`IUserRepository`. In a **unit** test we do not want real storage involved, so we replace
the repository with a **mock**: an object we create, tell how to behave, and then
interrogate afterwards about how it was used.

The guide describes this with Mockito's `@Mock` and `@InjectMocks` annotations. .NET has no
such annotations. You create the mock and pass it into the constructor yourself, which is
both simpler and more honest about what dependency injection actually is.

```csharp
var repository = new Mock<IUserRepository>();          // create the mock
repository.Setup(r => r.Exists("bobby")).Returns(false);   // tell it how to behave
var controller = new UserController(repository.Object);    // inject it

controller.Register(new User(0, "bobby", "Codes123"));

repository.Verify(r => r.Register(It.IsAny<User>()), Times.Once);   // check it was used
```

Mock all three repository methods as you need them: `Exists`, `Register` and `Login`.

**Stretch task, test driven:** plan and then build a real `ConcreteUserRepository`
implementing `IUserRepository`, storing its users in a `List<User>`. Write the test first,
watch it fail, write the smallest implementation that makes it pass, repeat. Skeletons are
in `Exercise3_Stretch_ConcreteUserRepositoryTests.cs`.

---

## Choices made in this translation, and why

**Target framework: `net9.0`.** Both .NET 9 and .NET 10 SDKs are available on the training
machines. .NET 9 is the one every installed SDK can build, so `global.json` pins the 9.0
band with `rollForward: latestFeature`. That way a machine with only .NET 10 is not stuck,
but everybody who has 9 builds identically. Nothing in these exercises uses a language
feature newer than C# 10.

**Test framework: xUnit.** It is the default for `dotnet new` test projects, so it is the
least friction here: the template already exists, the packages restore without extra
configuration, and `dotnet test` finds and runs it with no runner set-up. Its `[Fact]` and
`Assert.Throws<T>` map almost one to one onto the JUnit 5 `@Test` and `assertThrows` the
Java version uses, so students moving between the two versions are not relearning much.
`[Fact(Skip = "...")]` is what lets the unfinished skeleton ship green.

**Mocking library: Moq.** It is by a wide margin the most widely used mocking library in
.NET, so it is the one students will meet in real work, and it is the closest in feel to
the Mockito the guide describes: `Setup`/`Returns` reads like `when`/`thenReturn`, and
`Verify` like `verify`. NSubstitute would have done the job just as well, and its syntax is
arguably tidier, but familiarity wins for a teaching repository.

**`IllegalArgumentException` becomes `ArgumentException`.** Both are the language's standard
way of saying "a caller passed an argument value this method cannot accept", and both are
unchecked, so a test observes the same behaviour in either language. Where the Java throws a
bare `RuntimeException`, the C# throws `InvalidOperationException`, .NET's nearest
equivalent for "the object is not in a state where this can work". Each mapping is commented
in the source.

**Other C# conventions applied:** PascalCase method names, auto-properties instead of Java
getters and setters, and the `I` prefix on `IUserRepository`. That last one matters here:
it leaves the plain name `UserRepository` free, which is exactly why the stretch task's
class is called `ConcreteUserRepository` in the Java and could equally be called
`UserRepository` in C#. Nullable reference types are switched **off**, because several
exercises are about passing `null` in deliberately.

---

## For the trainer

`solutions/Exercises.Solutions` holds every test implemented and passing, plus a worked
`ConcreteUserRepository` for the stretch task.

It is deliberately **not** listed in `UnitTestExercises.sln`, so `dotnet build` and
`dotnet test` from this folder never touch it and a student cannot run it by accident.

To run the solutions:

```bash
dotnet test solutions/Exercises.Solutions
```

Read **CODE_CORRECTIONS.md** before the session. The Java original contained two real bugs.
Both are corrected here, with the reasoning commented in the source, and that file records
what was wrong, what changed, what a student should notice, and which tests cover it. It
also flags the one error still live in the exercise guide's own worked example, which is in
the worksheet rather than the code.

```
Passed!  - Failed:     0, Passed:    78, Skipped:     0, Total:    78
```
