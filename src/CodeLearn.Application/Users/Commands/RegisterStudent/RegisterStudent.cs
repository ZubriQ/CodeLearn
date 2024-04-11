using CodeLearn.Application.Common.IdentityModels;
using FluentValidation.Results;

namespace CodeLearn.Application.Users.Commands.RegisterStudent;

public record RegisterStudentCommand(UserFullName FullName, StudentUserDetails StudentDetails)
    : IRequest<OneOf<string, ValidationFailed>>;

public class RegisterStudentCommandHandler(
    IIdentityService _identityService,
    IValidator<RegisterStudentCommand> _validator,
    IApplicationDbContext _context)
    : IRequestHandler<RegisterStudentCommand, OneOf<string, ValidationFailed>>
{
    public async Task<OneOf<string, ValidationFailed>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        (var result, var userId) = await _identityService.CreateStudentUserAsync(request.FullName, request.StudentDetails);

        if (result.IsFailure)
        {
            return new ValidationFailed(new ValidationFailure("IdentityService", "CreateStudentUserAsync Failed"));
        }

        await _context.SaveChangesAsync(cancellationToken);

        return userId!;
    }
}