namespace CodeLearn.Contracts.Tests;

public record TestResponse(
    int TestId,
    string Title,
    string Description,
    bool IsPublic,
    DateTimeOffset Created,
    DateTimeOffset LastModified);