using CodeLearn.Application.TestingSessions.Commands.CreateTestingSession;
using CodeLearn.Application.TestingSessions.Queries.GetTestingSessionById;
using CodeLearn.Contracts.TestingSessions;

namespace CodeLearn.Api.Controllers;

public sealed class TestingSessionsController(ISender _sender, IMapper _mapper) : ApiControllerBase
{
    [HttpGet("{testingSessionId:int}")]
    [ProducesResponseType(typeof(TestingSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int testingSessionId)
    {
        var result = await _sender.Send(new GetTestingSessionByIdQuery(testingSessionId));

        return result.Match(
            testingSession => Ok(_mapper.Map<TestingSessionResponse>(testingSession)),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Testing session not found."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(TestingSessionRequest request)
    {
        var command = _mapper.Map<CreateTestingSessionCommand>(request);
        var result = await _sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Testing not found."));
    }
}