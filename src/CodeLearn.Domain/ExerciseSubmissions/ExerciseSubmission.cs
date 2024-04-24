namespace CodeLearn.Domain.ExerciseSubmissions;

public abstract class ExerciseSubmission(
    ExerciseId exerciseId,
    TestingSessionId testingSessionId,
    DateTimeOffset sentDateTime,
    SubmissionTestStatus status)
    : BaseEntity<ExerciseSubmissionId>(default!), IAggregateRoot
{
    public ExerciseId ExerciseId { get; private set; } = exerciseId;
    public TestingSessionId TestingSessionId { get; private set; } = testingSessionId;
    public DateTimeOffset SentDateTime { get; private set; } = sentDateTime;
    public SubmissionTestStatus Status { get; private set; } = status;
}