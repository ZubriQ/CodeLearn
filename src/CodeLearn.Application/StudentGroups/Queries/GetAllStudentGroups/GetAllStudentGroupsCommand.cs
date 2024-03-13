using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Application.StudentGroups.Queries.GetAllStudentGroups;

public record GetAllStudentGroupsCommand : IRequest<StudentGroup[]>;

public class GetAllStudentGroupsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllStudentGroupsCommand, StudentGroup[]>
{
    public async Task<StudentGroup[]> Handle(GetAllStudentGroupsCommand request, CancellationToken cancellationToken)
    {
        var studentGroups = await context.StudentGroups
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return studentGroups.Length == 0 ? [] : studentGroups;
    }
}