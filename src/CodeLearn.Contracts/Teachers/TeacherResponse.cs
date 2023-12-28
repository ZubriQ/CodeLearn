namespace CodeLearn.Contracts.Teachers;

public record TeacherResponse(
    Guid TeacherId,
    string FirstName,
    string LastName,
    string? Patronymic);