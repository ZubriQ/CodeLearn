using CodeLearn.Application.Testings.Commands.CreateTesting;
using CodeLearn.Application.Testings.Queries.GetAllTestings;
using CodeLearn.Application.Testings.Queries.GetTestingById;
using CodeLearn.Contracts.Testings;

namespace CodeLearn.Api.Controllers;

public sealed class TestingsController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpGet("{testingId:guid}")]
    [ProducesResponseType(typeof(TestingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid testingId)
    {
        var result = await sender.Send(new GetTestingByIdQuery(testingId));

        return result.Match(
            testing => Ok(mapper.Map<TestingResponse>(testing)),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Testing not found."));
    }

    [HttpGet]
    [ProducesResponseType(typeof(TestingResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var testings = await sender.Send(new GetAllTestingsQuery());
        var mappedTestings = testings.Select(mapper.Map<TestingResponse>).ToList();

        return Ok(new TestingResponseCollection(mappedTestings));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(TestingRequest request)
    {
        var command = mapper.Map<CreateTestingCommand>(request);
        var result = await sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }
}