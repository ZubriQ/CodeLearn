namespace CodeLearn.Contracts.Tests;

public record TestResponse(
    Guid TestId,
    string Title,
    string Description,
    int DurationInMinutes,
    bool IsPublic,
    DateTimeOffset Created,
    string? CreatedBy,
    DateTimeOffset LastModified,
    string? LastModifiedBy);