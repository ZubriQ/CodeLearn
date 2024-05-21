namespace CodeLearn.Contracts.Users.Students;

public record RegisterStudentRequest(
    string FirstName,
    string LastName,
    string? Patronymic,
    string StudentGroupName,
    string UserCode);