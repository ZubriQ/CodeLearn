using CodeLearn.Domain.StudentGroups;
using CodeLearn.Domain.StudentGroups.ValueObjects;

namespace CodeLearn.Application.StudentGroups.Queries.GetStudentGroupById;

public record GetStudentGroupByIdQuery(int Id) : IRequest<OneOf<StudentGroup, NotFound>>;

public class GetStudentGroupByIdCommandHandler(IApplicationDbContext context)
    : IRequestHandler<GetStudentGroupByIdQuery, OneOf<StudentGroup, NotFound>>
{
    public async Task<OneOf<StudentGroup, NotFound>> Handle(GetStudentGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var studentGroup = await context.StudentGroups
            .FirstOrDefaultAsync(t => t.Id == StudentGroupId.Create(request.Id), cancellationToken);

        if (studentGroup is null)
        {
            return new NotFound();
        }

        return studentGroup;
    }
}