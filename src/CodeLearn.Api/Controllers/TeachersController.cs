using CodeLearn.Application.Teachers.Commands.CreateTeacher;
using CodeLearn.Application.Teachers.Commands.DeleteTeacher;
using CodeLearn.Application.Teachers.Commands.UpdateTeacherName;
using CodeLearn.Application.Teachers.Queries.GetAllTeachers;
using CodeLearn.Application.Teachers.Queries.GetTeacherById;

namespace CodeLearn.Api.Controllers;

public sealed class TeachersController(ISender sender) : ApiControllerBase
{
    [HttpGet("{teacherId:guid}")]
    // [ProducesResponseType(typeof(TeacherResponseDto), StatusCodes.Status200OK)] // TODO: Mapping
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid teacherId)
    {
        var result = await sender.Send(new GetTeacherByIdQuery(teacherId));
        
        return result.Match(
            teacher => Ok(teacher),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Teacher not found."));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var allTeachers = await sender.Send(new GetAllTeachersCommand());
        
        return Ok(allTeachers);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTeacherCommand request)
    {
        var result = await sender.Send(request);
        
        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateName(UpdateTeacherNameCommand request)
    {
        var result = await sender.Send(request);

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Teacher not found."));
    }
    
    [HttpDelete("{teacherId:guid}")]
    public async Task<IActionResult> Delete(Guid teacherId)
    {
        var result  = await sender.Send(new DeleteTeacherCommand(teacherId));

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Teacher not found."));
    }
}