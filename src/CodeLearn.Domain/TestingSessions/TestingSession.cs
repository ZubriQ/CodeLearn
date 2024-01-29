namespace CodeLearn.Domain.TestingSessions;

public sealed class TestingSession : BaseAuditableEntity<TestingSessionId>, IAggregateRoot
{
    public TestId TestId { get; private set; } = null!;
    public int? Score { get; private set; }
    public TestingSessionStatus Status { get; private set; }
    public DateTime? StartDateTime { get; private set; }
    public DateTime? FinishDateTime { get; private set; }

    private TestingSession() { }

    private TestingSession(
        TestingSessionId id,
        TestId testId,
        int? score,
        TestingSessionStatus status,
        DateTime? startDateTime,
        DateTime? finishDateTime)
        : base(id)
    {
        TestId = testId;
        Score = score;
        Status = status;
        StartDateTime = startDateTime;
        FinishDateTime = finishDateTime;
    }

    public static TestingSession Create(
        TestId testId)
    {
        return new TestingSession(
            TestingSessionId.CreateUnique(),
            testId,
            default,
            TestingSessionStatus.Registered,
            default,
            default);
    }

    // TODO: StartTesting method

    // TODO: FinishTesting method
}