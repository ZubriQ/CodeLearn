using CodeLearn.Application.Users.Commands.ImportStudentList;
using CodeLearn.Application.Users.Commands.Login;
using CodeLearn.Application.Users.Commands.RefreshToken;
using CodeLearn.Application.Users.Commands.RegisterStudent;
using CodeLearn.Application.Users.Queries.GetAllStudents;
using CodeLearn.Contracts.Users;

namespace CodeLearn.Api.Controllers;

public sealed class UsersController(ISender _sender, IMapper _mapper) : ApiControllerBase
{
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            Ok,
            _ => Problem(statusCode: StatusCodes.Status403Forbidden, title: "Invalid credentials"));
    }

    [HttpPost]
    [Route("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var command = _mapper.Map<RefreshTokenCommand>(request);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            tokensDto => Ok(_mapper.Map<RefreshTokenResponse>(tokensDto)),
            _ => Problem(statusCode: StatusCodes.Status403Forbidden, title: "Invalid token."));
    }

    [HttpGet("students")]
    [ProducesResponseType(typeof(StudentResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStudents()
    {
        var queryResult = await _sender.Send(new GetAllStudentsQuery());
        var responseData = queryResult.Select(_mapper.Map<StudentResponse>).ToArray();

        return Ok(new StudentResponseCollection(responseData));
    }

    [HttpPost]
    [Route("students")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStudent([FromBody] RegisterStudentRequest request)
    {
        var command = _mapper.Map<RegisterStudentCommand>(request);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            id => CreatedAtAction(nameof(CreateStudent), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed"),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Student group not found"),
            _ => Problem(statusCode: StatusCodes.Status409Conflict, title: "Student with given Use code already exists"));
    }

    [HttpPost("students/import-list")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ImportStudentList([FromForm] ImportStudentListRequest request)
    {
        var fileData = new FileDataDto(request.File.FileName, request.File.OpenReadStream(), request.File.ContentType);
        var command = new ImportStudentListCommand(fileData, request.StudentGroupName);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(ImportStudentList), new { }, success),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed"),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Student group not found"));
    }

    //[HttpPost]
    //[Route("teachers")]
    //[ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateTeacher([FromBody] RegisterTeacherRequest request)
    //{
    //    var command = _mapper.Map<RegisterTeacherCommand>(request);
    //    var result = await _sender.Send(command);

    //    return result.Match<IActionResult>(
    //        id => CreatedAtAction(nameof(CreateTeacher), new { id }, id),
    //        _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed"));
    //}
}