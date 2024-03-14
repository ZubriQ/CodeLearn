using CodeLearn.Application.Tests.Commands.CreateTest;
using CodeLearn.Application.Tests.Commands.DeleteTest;
using CodeLearn.Application.Tests.Commands.UpdateTest;
using CodeLearn.Application.Tests.Queries.GetAllTests;
using CodeLearn.Application.Tests.Queries.GetTestById;
using CodeLearn.Contracts.Tests;

namespace CodeLearn.Api.Controllers;

public sealed class TestsController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpGet("{testId:int}")]
    [ProducesResponseType(typeof(TestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int testId)
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
        var result = await sender.Send(new GetAllTestsQuery());
        var mappedData = result
            .Select(mapper.Map<TestResponse>)
            .ToList();

        return Ok(new TestResponseCollection(mappedData));
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(TestRequest request)
    {
        var command = mapper.Map<CreateTestCommand>(request);
        var result = await sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }

    [HttpDelete("{testId:int}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int testId)
    {
        var result = await sender.Send(new DeleteTestCommand(testId));

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Test not found."));
    }

    [HttpPut("{testId:int}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int testId, UpdateTestDetailsRequest request)
    {
        var command = mapper.Map<UpdateTestCommand>((testId, request));
        var result = await sender.Send(command);

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Test not found."),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Invalid request."));
    }
}