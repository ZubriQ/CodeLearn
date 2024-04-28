namespace CodeLearn.Domain.Testings.Enums;

public enum TestingStatus
{
    None = 0,

    /// <summary>
    /// The testing is open for students to take the test.
    /// </summary>
    Open = 1,

    /// <summary>
    /// The testing is hidden and not accessible to students.
    /// </summary>
    Hidden = 2,

    /// <summary>
    /// The testing has been completed.
    /// </summary>
    Completed = 3,
}