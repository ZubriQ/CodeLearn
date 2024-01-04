namespace CodeLearn.Contracts.Tests;

public record TestResponse(
    Guid TestId,
    Guid TeacherId,
    string Title,
    string Description,
    int DurationInMinutes,
    bool IsPublic,
    string CreatedDateTime,
    string ModifiedDateTime);