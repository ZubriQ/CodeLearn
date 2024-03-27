namespace CodeLearn.Application.Authentication.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<OneOf<string, Forbid>>;

public class LoginCommandHandler(IIdentityService _identityService)
    : IRequestHandler<LoginCommand, OneOf<string, Forbid>>
{
    public async Task<OneOf<string, Forbid>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        (var result, var jwt) = await _identityService.Login(request.Email, request.Password);

        if (result.IsFailure)
        {
            return new Forbid();
        }

        return jwt!;
    }
}