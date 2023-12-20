namespace CodeLearn.Domain.ExerciseSubmissions.JunctionTables;

public sealed class SelectedChoice
{
    public ExerciseSubmissionId ExerciseSubmissionId { get; set; } = null!;
    public QuestionChoiceId QuestionChoiceId { get; set; } = null!;
}