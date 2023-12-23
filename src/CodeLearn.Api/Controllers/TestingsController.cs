using CodeLearn.Application.Testings.Commands.CreateTesting;

namespace CodeLearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TestingsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateTestingCommand request)
    {
        var testingId = await sender.Send(request);
        return Ok(testingId);
    }
}