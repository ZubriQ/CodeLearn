using CodeLearn.Application.Common.IdentityModels;
using FluentValidation.Results;

namespace CodeLearn.Application.Authentication.Commands.RegisterStudent;

public record RegisterStudentCommand(
    UserCredentials Credentials, UserFullName FullName, UserStudentDetails StudentDetails)
    : IRequest<OneOf<string, ValidationFailed>>;

public class RegisterStudentCommandHandler(
    IIdentityService _identityService, IValidator<RegisterStudentCommand> _validator)
    : IRequestHandler<RegisterStudentCommand, OneOf<string, ValidationFailed>>
{
    public async Task<OneOf<string, ValidationFailed>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        (var result, var userId) = await _identityService.CreateUserAsync(
            request.Credentials, request.FullName, request.StudentDetails);

        if (result.IsFailure)
        {
            return new ValidationFailed(new ValidationFailure("IdentityService", "CreateUserAsync Failed"));
        }

        return userId!;
    }
}