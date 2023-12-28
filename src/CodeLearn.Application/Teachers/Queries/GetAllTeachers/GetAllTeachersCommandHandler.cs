using CodeLearn.Domain.Teachers;

namespace CodeLearn.Application.Teachers.Queries.GetAllTeachers;

public class GetAllTeachersCommandHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllTeachersCommand, Teacher[]>
{
    public async Task<Teacher[]> Handle(GetAllTeachersCommand request, CancellationToken cancellationToken)
    {
        var teachers = await context.Teachers
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return teachers.Length == 0 ? [] : teachers;
    }
}