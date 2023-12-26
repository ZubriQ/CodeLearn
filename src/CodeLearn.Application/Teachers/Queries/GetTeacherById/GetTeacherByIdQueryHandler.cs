using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Teachers.ValueObjects;

namespace CodeLearn.Application.Teachers.Queries.GetTeacherById;

public class GetTeacherByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetTeacherByIdQuery, OneOf<Teacher, NotFound>>
{
    public async Task<OneOf<Teacher, NotFound>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var teacher = await context.Teachers
            .AsNoTracking()
            .Where(t => t.Id == TeacherId.Create(request.Id))
            .FirstOrDefaultAsync(cancellationToken);

        if (teacher is null)
        {
            return new NotFound();
        }

        return teacher;
    }
}