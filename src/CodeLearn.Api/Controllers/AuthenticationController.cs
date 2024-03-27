using CodeLearn.Application.Authentication.Commands.Login;
using CodeLearn.Application.Authentication.Commands.RegisterStudent;
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

    [HttpPost]
    [Route("register-student")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentRequest request)
    {
        var command = _mapper.Map<RegisterStudentCommand>(request);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            id => CreatedAtAction(nameof(RegisterStudent), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed"));
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            Ok,
            _ => Problem(statusCode: StatusCodes.Status403Forbidden, title: "Invalid credentials"));
    }
}