namespace CodeLearn.Application.ExerciseSubmissions.MethodCoding.Commands.CreateMethodCodingExerciseSubmission;

public record CodeExeciseSubmissionCreatedEvent
{
    public int Id { get; init; }

    public int TestingSessionId { get; init; }

    public int ExerciseId { get; init; }

    public string StudentCode { get; init; } = string.Empty;
};