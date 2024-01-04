namespace CodeLearn.Contracts.Tests;

public record TestRequest(
    Guid TeacherId,
    string Title,
    string Description,
    int DurationInMinutes);