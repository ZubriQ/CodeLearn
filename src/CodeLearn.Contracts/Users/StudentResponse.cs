namespace CodeLearn.Contracts.Users;

public record StudentResponse(
    string Id,
    string Email,
    string UserName,
    string FirstName,
    string LastName,
    string Patronymic,
    string WindowsAccountName,
    string StudentGroupName,
    string EnrolmentYear,
    string UserCode);