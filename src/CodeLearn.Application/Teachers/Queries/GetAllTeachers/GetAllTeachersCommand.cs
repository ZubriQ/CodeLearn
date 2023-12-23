namespace CodeLearn.Application.Teachers.Queries.GetAllTeachers;

public record GetAllTeachersCommand : IRequest<TeacherModel[]>;