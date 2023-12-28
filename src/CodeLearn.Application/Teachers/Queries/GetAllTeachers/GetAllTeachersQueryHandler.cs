using CodeLearn.Domain.Teachers;

namespace CodeLearn.Application.Teachers.Queries.GetAllTeachers;

public class GetAllTeachersQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllTeachersQuery, Teacher[]>
{
    public async Task<Teacher[]> Handle(GetAllTeachersQuery query, CancellationToken cancellationToken)
    {
        var teachers = await context.Teachers
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return teachers.Length == 0 ? [] : teachers;
    }
}