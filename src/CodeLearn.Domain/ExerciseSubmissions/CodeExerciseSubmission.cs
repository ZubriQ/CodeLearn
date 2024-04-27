namespace CodeLearn.Domain.ExerciseSubmissions;

public sealed class CodeExerciseSubmission : ExerciseSubmission
{
    public string StudentCode { get; private set; }
    public string? TestingInformation { get; private set; }
    public int RuntimeInMilliseconds { get; private set; }

    private CodeExerciseSubmission(
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTimeOffset sentDateTime,
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

    /// <summary>
    /// Student attempts to solve a method coding exercise.
    /// </summary>
    /// <param name="exerciseId">Method coding exercise Id.</param>
    /// <param name="testingSessionId">Testing session Id.</param>
    /// <param name="sentDateTime">DateTime attempt created.</param>
    /// <param name="studentCode">Written solution.</param>
    public static CodeExerciseSubmission Create(
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTimeOffset sentDateTime,
        string studentCode)
    {
        return new CodeExerciseSubmission(
            exerciseId,
            testingSessionId,
            sentDateTime,
            SubmissionTestStatus.Untested,
            studentCode,
            string.Empty,
            0);
    }

    public void SetTestingInformation(Result testingResult)
    {
        TestingInformation = testingResult.Error.Message;
    }
}