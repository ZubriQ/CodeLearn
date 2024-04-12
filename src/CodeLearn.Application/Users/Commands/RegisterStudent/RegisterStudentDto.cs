namespace CodeLearn.Application.Users.Commands.RegisterStudent;

public class RegisterStudentDto(
    string firstName,
    string lastName,
    string? patronymic,
    string studentGroupName,
    string userCode)
{
    public string FirstName { get; set; } = firstName;

    public string LastName { get; set; } = lastName;

    public string? Patronymic { get; set; } = patronymic;

    public string StudentGroupName { get; init; } = studentGroupName;

    public string UserCode { get; init; } = userCode;
}