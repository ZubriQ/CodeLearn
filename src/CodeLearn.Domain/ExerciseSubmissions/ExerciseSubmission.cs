namespace CodeLearn.Domain.ExerciseSubmissions;

public abstract class ExerciseSubmission(
    ExerciseSubmissionId id,
    ExerciseId exerciseId,
    DateTime sentDateTime,
    SubmissionTestStatus status)
    : BaseEntity<ExerciseSubmissionId>(id), IAggregateRoot
{
    public ExerciseId ExerciseId { get; private set; } = exerciseId;
    public DateTime SentDateTime { get; private set; } = sentDateTime;
    public SubmissionTestStatus Status { get; private set; } = status;
}