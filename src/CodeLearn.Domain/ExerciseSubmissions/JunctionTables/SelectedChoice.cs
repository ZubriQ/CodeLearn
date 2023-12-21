namespace CodeLearn.Domain.ExerciseSubmissions.JunctionTables;

public sealed class SelectedChoice
{
    public ExerciseSubmissionId ExerciseSubmissionId { get; init; } = null!;
    public QuestionChoiceId QuestionChoiceId { get; init; } = null!;
}