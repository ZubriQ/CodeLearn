using CodeLearn.Application.StudentGroups.Queries.GetAllStudentGroups;
using CodeLearn.Contracts.StudentGroups;

namespace CodeLearn.Api.Controllers;

public class StudentGroupsController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(StudentGroupResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var response = await sender.Send(new GetAllStudentGroupsQuery());
        var mappedData = response.Select(mapper.Map<StudentGroupResponse>).ToArray();

        return Ok(new StudentGroupResponseCollection(mappedData));
    }
}