using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Application.StudentGroups.Queries.GetAllStudentGroups;

public record GetAllStudentGroups : IRequest<StudentGroup[]>;

public class GetAllStudentGroupsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllStudentGroups, StudentGroup[]>
{
    public async Task<StudentGroup[]> Handle(GetAllStudentGroups request, CancellationToken cancellationToken)
    {
        var studentGroups = await context.StudentGroups
            .AsNoTracking()
            .ToArrayAsync();

        return studentGroups.Length == 0 ? [] : studentGroups;
    }
}