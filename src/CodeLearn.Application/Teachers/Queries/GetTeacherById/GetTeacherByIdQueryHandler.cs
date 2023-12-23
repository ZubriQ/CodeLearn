using CodeLearn.Domain.Teachers.ValueObjects;

namespace CodeLearn.Application.Teachers.Queries.GetTeacherById;

public class GetTeacherByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTeacherByIdQuery, TeacherModel>
{
    public async Task<TeacherModel> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await context.Teachers
            .AsNoTracking()
            .Where(t => t.Id == TeacherId.Create(request.Id))
            .FirstOrDefaultAsync(cancellationToken);

        // TODO: handle null

        var teacherModel = new TeacherModel(teacher.FirstName, teacher.LastName, teacher.Patronymic);

        return teacherModel;
    }
}