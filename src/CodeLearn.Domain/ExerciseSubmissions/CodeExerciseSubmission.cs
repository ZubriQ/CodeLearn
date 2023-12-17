namespace CodeLearn.Domain.ExerciseSubmissions;

public sealed class CodeExerciseSubmission : ExerciseSubmission
{
    public string StudentCode { get; private set; }
    public string? TestingInformation { get; private set; }

    private CodeExerciseSubmission(
        ExerciseSubmissionId id,
        ExerciseId exerciseId,
        DateTime sentDateTime,
        SubmissionTestStatus status,
        string studentCode,
        string testingInformation)
        : base(id, exerciseId, sentDateTime, status)
    {
        StudentCode = studentCode;
        TestingInformation = testingInformation;
    }

    public static CodeExerciseSubmission Create(
        ExerciseId exerciseId,
        DateTime sentDateTime,
        string studentCode,
        string testingInformation)
    {
        return new CodeExerciseSubmission(
            ExerciseSubmissionId.CreateUnique(),
            exerciseId,
            sentDateTime,
            SubmissionTestStatus.Untested,
            studentCode,
            testingInformation);
    }
}