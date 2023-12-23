using CodeLearn.Application.Teachers.Commands.CreateTeacher;
using CodeLearn.Application.Teachers.Queries.GetTeacherById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeLearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TeachersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateTeacherCommand request)
    {
        var teacherId = await sender.Send(request);
        return Ok(teacherId);
    }

    [HttpGet("{teacherId}")]
    public async Task<IActionResult> Get(Guid teacherId)
    {
        var teacherModel = await sender.Send(new GetTeacherByIdQuery(teacherId));
        return Ok(teacherModel);
    }
}
