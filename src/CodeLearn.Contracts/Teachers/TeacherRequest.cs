namespace CodeLearn.Contracts.Teachers;

public record TeacherRequest(
    string? FirstName,
    string? LastName,
    string? Patronymic);