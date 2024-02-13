namespace CodeLearn.Domain.ExerciseSubmissions;

public sealed class CodeExerciseSubmission : ExerciseSubmission
{
    public string StudentCode { get; private set; }
    public string? TestingInformation { get; private set; }
    public int RuntimeInMilliseconds { get; private set; }

    private CodeExerciseSubmission(
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTime sentDateTime,
        SubmissionTestStatus status,
        string studentCode,
        string testingInformation,
        int runtimeInMilliseconds)
        : base(exerciseId, testingSessionId, sentDateTime, status)
    {
        StudentCode = studentCode;
        TestingInformation = testingInformation;
        RuntimeInMilliseconds = runtimeInMilliseconds;
    }

    public static CodeExerciseSubmission Create(
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTime sentDateTime,
        string studentCode,
        string testingInformation,
        int runtimeInMilliseconds)
    {
        return new CodeExerciseSubmission(
            exerciseId,
            testingSessionId,
            sentDateTime,
            SubmissionTestStatus.Untested,
            studentCode,
            testingInformation,
            runtimeInMilliseconds);
    }
}