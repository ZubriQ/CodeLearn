namespace CodeLearn.Application.Teachers.Commands.UpdateTeacherName;

public record UpdateTeacherNameCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string? Patronymic)
    : IRequest<bool>;