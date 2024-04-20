namespace CodeLearn.Contracts.Testings;

public record TestingResponseForTestingSession(
    int Id,
    int TestId,
    DateTimeOffset DeadlineDate,
    int DurationInMinutes);