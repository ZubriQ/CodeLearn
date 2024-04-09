using CodeLearn.Application.Users.Queries.GetAllStudents;
using CodeLearn.Contracts.Users;

namespace CodeLearn.Api.Controllers;

public sealed class UsersController(ISender _sender, IMapper _mapper) : ApiControllerBase
{
    [HttpGet("students")]
    [ProducesResponseType(typeof(StudentResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStudents()
    {
        var queryResult = await _sender.Send(new GetAllStudentsQuery());
        var responseData = queryResult.Select(_mapper.Map<StudentResponse>).ToArray();

        return Ok(new StudentResponseCollection(responseData));
    }
}