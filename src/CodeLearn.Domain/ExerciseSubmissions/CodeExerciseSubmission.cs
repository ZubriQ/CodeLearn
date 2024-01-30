namespace CodeLearn.Domain.ExerciseSubmissions;

public sealed class CodeExerciseSubmission : ExerciseSubmission
{
    public string StudentCode { get; private set; }
    public string? TestingInformation { get; private set; }
    public TimeSpan Runtime { get; private set; }

    private CodeExerciseSubmission(
        ExerciseSubmissionId id,
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTime sentDateTime,
        SubmissionTestStatus status,
        string studentCode,
        string testingInformation)
        : base(id, exerciseId, testingSessionId, sentDateTime, status)
    {
        StudentCode = studentCode;
        TestingInformation = testingInformation;
    }

    public static CodeExerciseSubmission Create(
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTime sentDateTime,
        string studentCode,
        string testingInformation)
    {
        return new CodeExerciseSubmission(
            ExerciseSubmissionId.CreateUnique(),
            exerciseId,
            testingSessionId,
            sentDateTime,
            SubmissionTestStatus.Untested,
            studentCode,
            testingInformation);
    }
}