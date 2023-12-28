using CodeLearn.Application.Testings.Commands.CreateTesting;
using CodeLearn.Contracts.Testings;

namespace CodeLearn.Api.Controllers;

public sealed class TestingsController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(TestingRequest request)
    {
        var result = await sender.Send(mapper.Map<CreateTestingCommand>(request));

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }
}