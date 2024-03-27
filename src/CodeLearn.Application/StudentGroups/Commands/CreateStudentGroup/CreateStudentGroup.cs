using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Application.StudentGroups.Commands.CreateStudentGroup;

public record CreateStudentGroupCommand(string Name, int EnrolmentYear)
    : IRequest<OneOf<int, ValidationFailed>>;

public class CreateStudentGroupCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateStudentGroupCommand> _validator)
    : IRequestHandler<CreateStudentGroupCommand, OneOf<int, ValidationFailed>>
{
    public async Task<OneOf<int, ValidationFailed>> Handle(CreateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var studentGroup = StudentGroup.Create(request.Name, request.EnrolmentYear);

        _context.StudentGroups.Add(studentGroup);

        await _context.SaveChangesAsync(cancellationToken);

        return studentGroup.Id.Value;
    }
}