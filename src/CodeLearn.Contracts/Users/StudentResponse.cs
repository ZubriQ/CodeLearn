namespace CodeLearn.Contracts.Users;

public record StudentResponse(
    string Id,
    string UserName,
    string FullName,
    string StudentGroupName,
    int? EnrolmentYear,
    string UserCode,
    string TemporaryPassword);