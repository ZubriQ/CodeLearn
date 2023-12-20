namespace CodeLearn.Domain.ExerciseSubmissions;

public class ChoiceExerciseSubmission : ExerciseSubmission
{
    private readonly IList<QuestionChoice> _questionChoices = [];
    public virtual IReadOnlyList<QuestionChoice> QuestionChoices => _questionChoices.ToList();

    private ChoiceExerciseSubmission(
        ExerciseSubmissionId id,
        ExerciseId exerciseId,
        TestingSessionId testingSessionId,
        DateTime sentDateTime,
        SubmissionTestStatus status)
        : base(id, exerciseId, testingSessionId, sentDateTime, status) { }
}