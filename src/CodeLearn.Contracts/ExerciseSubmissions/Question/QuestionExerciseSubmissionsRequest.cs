namespace CodeLearn.Contracts.ExerciseSubmissions.Question;

/// <summary>
/// List of exercises with selected answers in them.
/// </summary>
/// <param name="SelectedChoices">ExerciseId : QuestionChoiceIds[]</param>
public record QuestionExerciseSubmissionsRequest(Dictionary<int, List<int>> SelectedChoices);