namespace CodeLearn.Contracts.Tests;

public record TestResponse(
    int Id,
    string Title,
    string Description,
    bool IsPublic,
    DateTimeOffset Created,
    DateTimeOffset LastModified);