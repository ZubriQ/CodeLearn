using CodeLearn.Application.Teachers.Commands.CreateTeacher;
using CodeLearn.Application.Teachers.Commands.DeleteTeacher;
using CodeLearn.Application.Teachers.Commands.UpdateTeacherName;
using CodeLearn.Application.Teachers.Queries.GetTeacherById;

namespace CodeLearn.Api.Controllers;

[ApiController]
[Route("api/teachers")]
public sealed class TeachersController(ISender sender) : ControllerBase
{
    [HttpGet("{teacherId}")]
    public async Task<IActionResult> Get(Guid teacherId)
    {
        var teacherModel = await sender.Send(new GetTeacherByIdQuery(teacherId));
        return Ok(teacherModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTeacherCommand request)
    {
        var teacherId = await sender.Send(request);
        return Ok(teacherId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateName(UpdateTeacherNameCommand request)
    {
        var isSuccess = await sender.Send(request);

        return isSuccess
            ? Ok(isSuccess)
            : NotFound();
    }

    [HttpDelete("{teacherId}")]
    public async Task<IActionResult> Delete(Guid teacherId)
    {
        var isSuccess = await sender.Send(new DeleteTeacherCommand(teacherId));

        return isSuccess
            ? Ok(isSuccess)
            : NotFound();
    }
}