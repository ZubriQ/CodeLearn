using CodeLearn.Application.StudentGroups.Commands.CreateStudentGroup;
using CodeLearn.Application.StudentGroups.Commands.DeleteStudentGroup;
using CodeLearn.Application.StudentGroups.Commands.UpdateStudentGroup;
using CodeLearn.Application.StudentGroups.Queries.GetAllStudentGroups;
using CodeLearn.Application.StudentGroups.Queries.GetStudentGroupById;
using CodeLearn.Contracts.StudentGroups;

namespace CodeLearn.Api.Controllers;

public sealed class StudentGroupsController(ISender _sender, IMapper _mapper) : ApiControllerBase
{
    [HttpGet("{studentGroupId:int}")]
    [ProducesResponseType(typeof(StudentGroupResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int studentGroupId)
    {
        var result = await _sender.Send(new GetStudentGroupByIdQuery(studentGroupId));

        return result.Match(
            studentGroup => Ok(_mapper.Map<StudentGroupResponse>(studentGroup)),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Student group not found."));
    }

    [HttpGet]
    [ProducesResponseType(typeof(StudentGroupResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _sender.Send(new GetAllStudentGroupsQuery());
        var mappedData = response.Select(_mapper.Map<StudentGroupResponse>).ToArray();

        return Ok(new StudentGroupResponseCollection(mappedData));
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(StudentGroupRequest request)
    {
        var command = _mapper.Map<CreateStudentGroupCommand>(request);
        var result = await _sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }

    [HttpDelete("{studentGroupId:int}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int studentGroupId)
    {
        var result = await _sender.Send(new DeleteStudentGroupCommand(studentGroupId));

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Student group not found."));
    }

    [HttpPut("{studentGroupId:int}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int studentGroupId, StudentGroupRequest request)
    {
        var command = _mapper.Map<UpdateStudentGroupCommand>((studentGroupId, request));
        var result = await _sender.Send(command);

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Student group not found."),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Invalid request."));
    }
}