using CodeLearn.Application.Testings.Commands.CreateTesting;

namespace CodeLearn.Api.Controllers;

public sealed class TestingsController(ISender sender) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateTestingCommand request)
    {
        var testingId = await sender.Send(request);
        return Ok(testingId);
    }
}