namespace CodeLearn.Contracts.Users.Teachers;

public record RegisterTeacherRequest(
    string FirstName,
    string LastName,
    string? Patronymic);