using CodeLearn.Application.StudentGroups.Commands.CreateStudentGroup;
using CodeLearn.Application.StudentGroups.Commands.DeleteStudentGroup;
using CodeLearn.Application.StudentGroups.Queries.GetAllStudentGroups;
using CodeLearn.Contracts.StudentGroups;

namespace CodeLearn.Api.Controllers;

public class StudentGroupsController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(StudentGroupResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var response = await sender.Send(new GetAllStudentGroups());
        var mappedData = response.Select(mapper.Map<StudentGroupResponse>).ToArray();

        return Ok(new StudentGroupResponseCollection(mappedData));
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(StudentGroupRequest request)
    {
        var command = mapper.Map<CreateStudentGroupCommand>(request);
        var result = await sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }

    [HttpDelete("{studentGroupId:int}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int studentGroupId)
    {
        var result = await sender.Send(new DeleteStudentGroupCommand(studentGroupId));

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Student group not found."));
    }
}