using CodeLearn.Domain.Teachers;

namespace CodeLearn.Application.Teachers.Queries.GetAllTeachers;

public record GetAllTeachersCommand : IRequest<Teacher[]>;