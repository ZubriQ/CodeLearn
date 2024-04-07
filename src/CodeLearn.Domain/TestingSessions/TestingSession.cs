namespace CodeLearn.Domain.TestingSessions;

public sealed class TestingSession : BaseAuditableEntity<TestingSessionId>, IAggregateRoot
{
    public TestingId TestingId { get; private set; }
    public TestingSessionStatus Status { get; private set; }
    public DateTimeOffset StartDateTime { get; private set; }
    public DateTimeOffset FinishDateTime { get; private set; }
    public int Score { get; private set; }

    private TestingSession(
        TestingId testingId,
        TestingSessionStatus status,
        DateTimeOffset startDateTime,
        DateTimeOffset finishDateTime)
        : base(default!)
    {
        TestingId = testingId;
        Status = status;
        StartDateTime = startDateTime;
        FinishDateTime = finishDateTime;
        Score = 0;
    }

    /// <summary>
    /// Used to start testing.
    /// </summary>
    public static TestingSession Create(
        TestingId testingId,
        DateTimeOffset startDateTime,
        DateTimeOffset finishDateTime)
    {
        return new TestingSession(
            testingId,
            TestingSessionStatus.InProgress,
            startDateTime,
            finishDateTime);
    }

    // TODO: FinishTesting method
}