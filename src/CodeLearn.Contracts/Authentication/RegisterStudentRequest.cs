namespace CodeLearn.Contracts.Authentication;

public record RegisterStudentRequest(
    string FirstName,
    string LastName,
    string? Patronymic,
    string Email,
    string Password,
    string StudentGroupName,
    int EnrolmentYear);