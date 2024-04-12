using FluentValidation.Results;

namespace CodeLearn.Application.Users.Commands.RegisterStudent;

public record RegisterStudentCommand(RegisterStudentDto Student)
    : IRequest<OneOf<string, ValidationFailed, NotFound, Conflict>>;

public class RegisterStudentCommandHandler(
    IIdentityService _identityService,
    IValidator<RegisterStudentCommand> _validator,
    IApplicationDbContext _context)
    : IRequestHandler<RegisterStudentCommand, OneOf<string, ValidationFailed, NotFound, Conflict>>
{
    public async Task<OneOf<string, ValidationFailed, NotFound, Conflict>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var studentGroupExists = await _context.StudentGroups
            .AnyAsync(x => x.Name == request.Student.StudentGroupName, cancellationToken);

        if (!studentGroupExists)
        {
            return new NotFound();
        }

        var studentExists = await _identityService.UserCodeExistsAsync(request.Student.UserCode);

        if (studentExists)
        {
            return new Conflict();
        }

        (var result, var userId) = await _identityService.CreateStudentUserAsync(request.Student);

        if (result.IsFailure)
        {
            return new ValidationFailed(new ValidationFailure("IdentityService", "CreateStudentUserAsync Failed"));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return userId!;
    }
}