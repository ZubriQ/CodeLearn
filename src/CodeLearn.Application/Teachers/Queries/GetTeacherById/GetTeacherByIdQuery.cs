namespace CodeLearn.Application.Teachers.Queries.GetTeacherById;

public record GetTeacherByIdQuery(Guid Id) : IRequest<TeacherModel>;