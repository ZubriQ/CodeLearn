namespace CodeLearn.Contracts.Testings;

public record TestingRequest(
    int TestId,
    int StudentGroupId,
    DateTimeOffset DeadlineDate,
    int DurationInMinutes);