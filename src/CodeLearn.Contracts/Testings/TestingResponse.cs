namespace CodeLearn.Contracts.Testings;

public record TestingResponse(
    int Id,
    int TestId,
    string TestTitle,
    int StudentGroupId,
    string StudentGroupName,
    DateTime StartDateTime,
    DateTime EndDateTime,
    int DurationInMinutes);