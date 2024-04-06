namespace CodeLearn.Application.Testings.Queries.GetAllTestings;

public record TestingDto(
    int Id,
    int TestId,
    string? TestTitle,
    int StudentGroupId,
    string? StudentGroupName,
    DateTimeOffset DeadlineDate,
    int DurationInMinutes);