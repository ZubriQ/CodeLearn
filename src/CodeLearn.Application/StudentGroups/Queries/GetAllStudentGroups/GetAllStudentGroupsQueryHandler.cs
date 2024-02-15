using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Application.StudentGroups.Queries.GetAllStudentGroups;

public class GetAllStudentGroupsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllStudentGroupsQuery, StudentGroup[]>
{
    public async Task<StudentGroup[]> Handle(GetAllStudentGroupsQuery request, CancellationToken cancellationToken)
    {
        var studentGroups = await context.StudentGroups
            .AsNoTracking()
            .ToArrayAsync();

        return studentGroups.Length == 0 ? [] : studentGroups;
    }
}