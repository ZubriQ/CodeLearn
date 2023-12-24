using CodeLearn.Domain.Teachers;

namespace CodeLearn.Application.Teachers.Queries.GetTeacherById;

public record GetTeacherByIdQuery(Guid Id) : IRequest<OneOf<Teacher, NotFound>>;