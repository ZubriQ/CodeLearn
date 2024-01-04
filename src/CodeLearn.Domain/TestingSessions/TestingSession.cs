namespace CodeLearn.Domain.TestingSessions;

public sealed class TestingSession : BaseEntity<TestingSessionId>, IAggregateRoot
{
    public TestId TestId { get; private set; } = null!;
    public StudentId StudentId { get; private set; } = null!;
    public int Score { get; private set; }
    public TestingSessionStatus Status { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime FinishDateTime { get; private set; }

    private TestingSession() { }

    private TestingSession(
        TestingSessionId id,
        TestId testId,
        StudentId studentId,
        int score,
        TestingSessionStatus status,
        DateTime startDateTime,
        DateTime finishDateTime)
        : base(id)
    {
        TestId = testId;
        StudentId = studentId;
        Score = score;
        Status = status;
        StartDateTime = startDateTime;
        FinishDateTime = finishDateTime;
    }

    public static TestingSession Create(TestId testId,
        StudentId studentId,
        int score,
        DateTime startDateTime,
        DateTime finishDateTime)
    {
        return new TestingSession(
            TestingSessionId.CreateUnique(),
            testId,
            studentId,
            score,
            TestingSessionStatus.NotStarted,
            startDateTime,
            finishDateTime);
    }
}