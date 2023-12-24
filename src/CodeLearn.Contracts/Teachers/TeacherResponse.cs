namespace CodeLearn.Contracts.Teachers;

public record TeacherResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string? Patronymic);