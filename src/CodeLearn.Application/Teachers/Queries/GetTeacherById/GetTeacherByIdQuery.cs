using CodeLearn.Domain.Teachers;

namespace CodeLearn.Application.Teachers.Queries.GetTeacherById;

public record GetTeacherByIdQuery(Guid TeacherId) : IRequest<OneOf<Teacher, NotFound>>;