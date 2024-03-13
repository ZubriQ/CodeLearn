using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CodeLearn.Api.Controllers;

[AllowAnonymous]
[Route("api/auth")]
public sealed class AuthenticationController : ApiControllerBase
{
    private readonly IIdentityService _identityService;

    public AuthenticationController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        (var result, var userId) = await _identityService.CreateUserAsync(request.Email, request.Password, request.Role);

        if (result.IsFailure)
        {
            return BadRequest();
        }

        return Ok(userId);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        (var result, var jwtToken) = await _identityService.Login(request.Email, request.Password);

        if (result.IsFailure)
        {
            return Forbid();
        }

        return Ok(jwtToken);
    }
}