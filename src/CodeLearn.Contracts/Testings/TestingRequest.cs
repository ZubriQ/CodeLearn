namespace CodeLearn.Contracts.Testings;

public record TestingRequest(
    int TestId,
    int StudentGroupId,
    DateTime StartDateTime,
    int DurationInMinutes);