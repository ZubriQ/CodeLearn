namespace CodeLearn.Application.ExerciseSubmissions.MethodCoding.Commands.CreateExerciseSubmission;

public record CodeExeciseSubmissionCreatedEvent
{
    public int Id { get; init; }

    public int ExerciseId { get; init; }

    public string StudentCode { get; init; } = string.Empty;
};