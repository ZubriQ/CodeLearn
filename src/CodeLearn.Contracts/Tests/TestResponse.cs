namespace CodeLearn.Contracts.Tests;

public record TestResponse(
    Guid TestId,
    string Title,
    string Description,
    bool IsPublic,
    DateTimeOffset Created,
    string? CreatedBy,
    DateTimeOffset LastModified,
    string? LastModifiedBy);