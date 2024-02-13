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
}