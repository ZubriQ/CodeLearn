using CodeLearn.Application.Common.Models;

namespace CodeLearn.Application.Users.Commands.RefreshToken;

public record RefreshTokenCommand(string JwtToken, string RefreshToken) : IRequest<OneOf<TokensDto, Unauthorised>>;

public class RefreshTokenCommandHandler(IIdentityService _identityService)
    : IRequestHandler<RefreshTokenCommand, OneOf<TokensDto, Unauthorised>>
{
    public async Task<OneOf<TokensDto, Unauthorised>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        (var result, var tokensDto) = await _identityService.RefreshToken(request.JwtToken, request.RefreshToken);

        if (result.IsFailure)
        {
            return new Unauthorised();
        }

        return tokensDto!;
    }
}