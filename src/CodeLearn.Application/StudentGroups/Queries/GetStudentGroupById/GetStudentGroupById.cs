using CodeLearn.Domain.StudentGroups;
using CodeLearn.Domain.StudentGroups.ValueObjects;

namespace CodeLearn.Application.StudentGroups.Queries.GetStudentGroupById;

public record GetStudentGroupByIdCommand(int Id) : IRequest<OneOf<StudentGroup, NotFound>>;

public class GetStudentGroupByIdCommandHandler(IApplicationDbContext context)
    : IRequestHandler<GetStudentGroupByIdCommand, OneOf<StudentGroup, NotFound>>
{
    public async Task<OneOf<StudentGroup, NotFound>> Handle(GetStudentGroupByIdCommand request, CancellationToken cancellationToken)
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