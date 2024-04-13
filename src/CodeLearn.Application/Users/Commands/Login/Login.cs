using CodeLearn.Application.Common.Models;

namespace CodeLearn.Application.Users.Commands.Login;

public record LoginCommand(string Username, string Password) : IRequest<OneOf<TokensDto, Forbid>>;

public class LoginCommandHandler(IIdentityService _identityService)
    : IRequestHandler<LoginCommand, OneOf<TokensDto, Forbid>>
{
    public async Task<OneOf<TokensDto, Forbid>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        (var result, var tokensDto) = await _identityService.Login(request.Username, request.Password);

        if (result.IsFailure)
        {
            return new Forbid();
        }

        return tokensDto!;
    }
}