namespace CodeLearn.Domain.ExerciseSubmissions;

public sealed class ChoiceExerciseSubmission : ExerciseSubmission
{
    private readonly IList<QuestionChoice> _choices = [];
    public IReadOnlyList<QuestionChoice> Choices => _choices.ToList();

    private ChoiceExerciseSubmission(
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTime sentDateTime,
        SubmissionTestStatus status)
        : base(exerciseId, testingSessionId, sentDateTime, status) { }

    public static ChoiceExerciseSubmission Create(
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTime sentDateTime)
    {
        return new ChoiceExerciseSubmission(
            exerciseId,
            testingSessionId,
            sentDateTime,
            SubmissionTestStatus.Untested);
    }

    // TODO: Add answer (choice) method?
}