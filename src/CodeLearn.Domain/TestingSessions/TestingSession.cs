namespace CodeLearn.Domain.TestingSessions;

public sealed class TestingSession : BaseAuditableEntity<TestingSessionId>, IAggregateRoot
{
    public TestingId TestingId { get; private set; } = null!;
    public TestingSessionStatus Status { get; private set; }
    public DateTime? StartDateTime { get; private set; }
    public DateTime? FinishDateTime { get; private set; }
    public int? Score { get; private set; }

    private TestingSession() { }

    private TestingSession(
        TestingSessionId id,
        TestingId testingId,
        TestingSessionStatus status,
        DateTime? startDateTime,
        DateTime? finishDateTime,
        int? score)
        : base(id)
    {
        TestingId = testingId;
        Score = score;
        Status = status;
        StartDateTime = startDateTime;
        FinishDateTime = finishDateTime;
    }

    /// <summary>
    /// Used to register for testing.
    /// </summary>
    public static TestingSession Create(
        TestingId testingId)
    {
        return new TestingSession(
            TestingSessionId.CreateUnique(),
            testingId,
            TestingSessionStatus.Registered,
            default,
            default,
            null);
    }

    // TODO: StartTesting method

    // TODO: FinishTesting method
}