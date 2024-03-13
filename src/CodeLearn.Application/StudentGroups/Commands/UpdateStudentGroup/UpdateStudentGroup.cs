using CodeLearn.Domain.StudentGroups.ValueObjects;

namespace CodeLearn.Application.StudentGroups.Commands.UpdateStudentGroup;

public record UpdateStudentGroupCommand(int Id, string Name, int EnrolmentYear)
    : IRequest<OneOf<Success, NotFound, BadRequest>>;

public class UpdateStudentGroupCommandHandler(IApplicationDbContext context)
    : IRequestHandler<UpdateStudentGroupCommand, OneOf<Success, NotFound, BadRequest>>
{
    public async Task<OneOf<Success, NotFound, BadRequest>> Handle(UpdateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var studentGroup = await context.StudentGroups
            .FirstOrDefaultAsync(x => x.Id == StudentGroupId.Create(request.Id), cancellationToken);

        if (studentGroup is null)
        {
            return new NotFound();
        }

        var result = studentGroup.UpdateDetails(request.Name, request.EnrolmentYear);

        if (result.IsFailure)
        {
            return new BadRequest();
        }

        await context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}