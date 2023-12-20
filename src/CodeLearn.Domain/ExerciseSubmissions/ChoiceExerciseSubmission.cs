namespace CodeLearn.Domain.ExerciseSubmissions;

public class ChoiceExerciseSubmission : ExerciseSubmission
{
    private readonly IList<QuestionChoice> _choices = [];
    public virtual IReadOnlyList<QuestionChoice> Choices => _choices.ToList();

    private ChoiceExerciseSubmission(
        ExerciseSubmissionId id,
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTime sentDateTime,
        SubmissionTestStatus status)
        : base(id, exerciseId, testingSessionId, sentDateTime, status) { }
}