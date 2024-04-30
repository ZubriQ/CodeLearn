namespace CodeLearn.Domain.TestingSessions;

public sealed class TestingSession : BaseAuditableEntity<TestingSessionId>, IAggregateRoot
{
    public TestingId TestingId { get; private set; }
    public TestingSessionStatus Status { get; private set; }
    public DateTimeOffset StartDateTime { get; private set; }
    public DateTimeOffset FinishDateTime { get; private set; }
    public int CorrectQuestionsCount { get; private set; }
    public int SolvedExerecisesCount { get; private set; }
    public string StudentFeedback { get; private set; } = string.Empty;

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

    public void Finish()
    {
        Status = TestingSessionStatus.Finished;
    }

    public void FinishTestingSession(string studentFeedback)
    {
        Status = TestingSessionStatus.Finished;
        StudentFeedback = studentFeedback;
    }

    public void SetSolvedExercises(int correctQuestions, int solvedExercises) // TODO: Result, validation
    {
        CorrectQuestionsCount = correctQuestions;
        SolvedExerecisesCount = solvedExercises;
    }
}