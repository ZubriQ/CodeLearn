using CodeLearn.Application.Common.IdentityModels;
using FluentValidation.Results;

namespace CodeLearn.Application.Authentication.Commands.RegisterTeacher;

public record RegisterTeacherCommand(UserCredentials Credentials, UserFullName FullName)
    : IRequest<OneOf<string, ValidationFailed>>;

public class RegisterTeacherCommandHandler(
    IIdentityService _identityService, IValidator<RegisterTeacherCommand> _validator)
    : IRequestHandler<RegisterTeacherCommand, OneOf<string, ValidationFailed>>
{
    public async Task<OneOf<string, ValidationFailed>> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        (var result, var userId) = await _identityService.CreateUserAsync(request.Credentials, request.FullName);

        if (result.IsFailure)
        {
            return new ValidationFailed(new ValidationFailure("IdentityService", "CreateUserAsync Failed"));
        }

        return userId!;
    }
}