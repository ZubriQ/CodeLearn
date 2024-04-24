namespace CodeLearn.Domain.ExerciseSubmissions;

public sealed class ChoiceExerciseSubmission : ExerciseSubmission
{
    private readonly IList<QuestionChoice> _choices = [];
    public IReadOnlyList<QuestionChoice> Choices => _choices.ToList();

    private ChoiceExerciseSubmission(
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTimeOffset sentDateTime,
        SubmissionTestStatus status)
        : base(exerciseId, testingSessionId, sentDateTime, status) { }

    public static ChoiceExerciseSubmission Create(
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTimeOffset sentDateTime)
    {
        return new ChoiceExerciseSubmission(
            exerciseId,
            testingSessionId,
            sentDateTime,
            SubmissionTestStatus.Untested);
    }

    public void AddChoice(QuestionChoice choice)
    {
        _choices.Add(choice);
    }
}