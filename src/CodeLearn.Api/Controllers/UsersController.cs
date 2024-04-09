using CodeLearn.Application.Users.Commands.Login;
using CodeLearn.Application.Users.Commands.RegisterStudent;
using CodeLearn.Application.Users.Queries.GetAllStudents;
using CodeLearn.Contracts.Users;

namespace CodeLearn.Api.Controllers;

public sealed class UsersController(ISender _sender, IMapper _mapper) : ApiControllerBase
{
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
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed"));
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