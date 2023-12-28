using CodeLearn.Application.Teachers.Commands.CreateTeacher;
using CodeLearn.Application.Teachers.Commands.DeleteTeacher;
using CodeLearn.Application.Teachers.Commands.UpdateTeacherName;
using CodeLearn.Application.Teachers.Queries.GetAllTeachers;
using CodeLearn.Application.Teachers.Queries.GetTeacherById;
using CodeLearn.Contracts.Teachers;

namespace CodeLearn.Api.Controllers;

public sealed class TeachersController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpGet("{teacherId:guid}")]
    [ProducesResponseType(typeof(TeacherResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid teacherId)
    {
        var result = await sender.Send(new GetTeacherByIdQuery(teacherId));

        return result.Match(
            teacher => Ok(mapper.Map<TeacherResponse>(teacher)),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Teacher not found."));
    }

    [HttpGet]
    [ProducesResponseType(typeof(TeacherResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var teachers = await sender.Send(new GetAllTeachersQuery());
        var mappedTeachers = teachers.Select(mapper.Map<TeacherResponse>).ToList();

        return Ok(new TeacherResponseCollection(mappedTeachers));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(TeacherRequest request)
    {
        var command = mapper.Map<CreateTeacherCommand>(request);
        var result = await sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }

    [HttpPut("{teacherId:guid}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateName(Guid teacherId, TeacherRequest request)
    {
        var command = mapper.Map<UpdateTeacherNameCommand>((teacherId, request));
        var result = await sender.Send(command);

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Teacher not found."),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Invalid request."));
    }

    [HttpDelete("{teacherId:guid}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid teacherId)
    {
        var result = await sender.Send(new DeleteTeacherCommand(teacherId));

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Teacher not found."));
    }
}