namespace CodeLearn.Contracts.TestingSessions;

public record TestingSessionResponse(
    int Id,
    int TestingId,
    string Status,
    string StartDateTime,
    string FinishDateTime,
    int Score);