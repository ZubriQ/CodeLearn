namespace CodeLearn.Contracts.Tests;

public record TestResponse(
    int TestId,
    string Title,
    string Description,
    DateTimeOffset Created,
    string? CreatedBy,
    DateTimeOffset LastModified,
    string? LastModifiedBy);