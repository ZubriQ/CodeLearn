namespace CodeLearn.Application.Teachers.Commands.DeleteTeacher;

public record DeleteTeacherCommand(Guid Id) : IRequest<OneOf<Success, NotFound>>;