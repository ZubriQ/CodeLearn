namespace CodeLearn.Contracts.Users;

public record RegisterStudentRequest(
    string FirstName,
    string LastName,
    string? Patronymic,
    string StudentGroupName,
    string UserCode);