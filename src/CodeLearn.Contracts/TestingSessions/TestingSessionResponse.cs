namespace CodeLearn.Contracts.TestingSessions;

public record TestingSessionResponse(
    int Id,
    int TestingId,
    string Status,
    DateTimeOffset StartDateTime,
    DateTimeOffset FinishDateTime,
    int Score);