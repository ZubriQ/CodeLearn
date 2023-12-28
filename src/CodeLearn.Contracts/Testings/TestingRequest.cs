namespace CodeLearn.Contracts.Testings;

public record TestingRequest(
    Guid TeacherId,
    string Title,
    string Description,
    int DurationInMinutes);