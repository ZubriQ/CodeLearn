namespace CodeLearn.Contracts.ExerciseSubmissions.MethodCoding;

public record MethodCodingExerciseSubmissionRequest(
    int ExerciseId,
    string MethodSolutionCode);