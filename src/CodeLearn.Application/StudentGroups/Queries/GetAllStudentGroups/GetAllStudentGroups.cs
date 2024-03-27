using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Application.StudentGroups.Queries.GetAllStudentGroups;

public record GetAllStudentGroupsQuery : IRequest<StudentGroup[]>;

public class GetAllStudentGroupsQueryHandler(IApplicationDbContext _context)
    : IRequestHandler<GetAllStudentGroupsQuery, StudentGroup[]>
{
    public async Task<StudentGroup[]> Handle(GetAllStudentGroupsQuery request, CancellationToken cancellationToken)
    {
        var studentGroups = await _context.StudentGroups
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return studentGroups.Length == 0 ? [] : studentGroups;
    }
}