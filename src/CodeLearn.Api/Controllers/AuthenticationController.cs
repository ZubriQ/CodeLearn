using CodeLearn.Application.Authentication.Commands.RegisterTeacher;
using CodeLearn.Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CodeLearn.Api.Controllers;

[AllowAnonymous]
[Route("api/auth")]
public sealed class AuthenticationController(ISender _sender, IMapper _mapper) : ApiControllerBase
{
    [HttpPost]
    [Route("register-teacher")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterTeacher([FromBody] RegisterTeacherRequest request)
    {
        var command = _mapper.Map<RegisterTeacherCommand>(request);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            id => CreatedAtAction(nameof(RegisterTeacher), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed"));
    }

    //[HttpPost]
    //[Route("login")]
    //public async Task<IActionResult> Login([FromBody] LoginRequest request)
    //{
    //    (var result, var jwtToken) = await _identityService.Login(request.Email, request.Password);

    //    if (result.IsFailure)
    //    {
    //        return Forbid();
    //    }

    //    return Ok(jwtToken);
    //}
}