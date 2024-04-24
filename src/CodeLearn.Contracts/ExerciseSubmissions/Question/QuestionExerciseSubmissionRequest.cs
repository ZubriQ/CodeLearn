namespace CodeLearn.Contracts.ExerciseSubmissions.Question;

public record QuestionExerciseSubmissionRequest(int ExerciseId, int[] SelectedAnswers);