namespace CodeLearn.Application.Teachers.Commands.CreateTeacher;

public record CreateTeacherCommand(
    string FirstName,
    string LastName,
    string? Patronymic)
    : IRequest<Guid>;