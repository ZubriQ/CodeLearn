namespace CodeLearn.Contracts.Testings;

public record TestingResponse(
    Guid TestingId,
    Guid TeacherId,
    string Title,
    string Description,
    int DurationInMinutes,
    bool IsPublic,
    DateTime CreatedDateTime,
    DateTime ModifiedDateTime);