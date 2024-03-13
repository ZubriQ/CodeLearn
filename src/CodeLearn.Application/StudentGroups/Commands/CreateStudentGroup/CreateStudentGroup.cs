using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Application.StudentGroups.Commands.CreateStudentGroup;

public record CreateStudentGroupCommand(
    string Name,
    int EnrolmentYear)
    : IRequest<OneOf<int, ValidationFailed>>;

public class CreateStudentGroupCommandHandler(
    IApplicationDbContext context,
    IValidator<CreateStudentGroupCommand> validator)
    : IRequestHandler<CreateStudentGroupCommand, OneOf<int, ValidationFailed>>
{
    public async Task<OneOf<int, ValidationFailed>> Handle(CreateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var studentGroup = StudentGroup.Create(request.Name, request.EnrolmentYear);

        context.StudentGroups.Add(studentGroup);

        await context.SaveChangesAsync(cancellationToken);

        return studentGroup.Id.Value;
    }
}