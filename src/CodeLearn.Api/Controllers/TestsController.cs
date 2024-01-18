using CodeLearn.Application.Tests.Commands.CreateTest;
using CodeLearn.Application.Tests.Queries.GetAllTests;
using CodeLearn.Application.Tests.Queries.GetTestById;
using CodeLearn.Contracts.Tests;

namespace CodeLearn.Api.Controllers;

public sealed class TestsController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpGet("{testId:guid}")]
    [ProducesResponseType(typeof(TestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid testId)
    {
        var result = await sender.Send(new GetTestByIdQuery(testId));

        return result.Match(
            test => Ok(mapper.Map<TestResponse>(test)),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Test not found."));
    }

    [HttpGet]
    [ProducesResponseType(typeof(TestResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var tests = await sender.Send(new GetAllTestsQuery());
        var mappedTestings = tests
            .Select(mapper.Map<TestResponse>)
            .ToList();

        return Ok(new TestResponseCollection(mappedTestings));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(TestRequest request)
    {
        var command = mapper.Map<CreateTestCommand>(request);
        var result = await sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }
}