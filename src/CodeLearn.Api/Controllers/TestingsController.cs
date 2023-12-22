using CodeLearn.Application.Testings.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeLearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestingsController : ControllerBase
{
    private readonly ISender _mediator;

    public TestingsController(ISender mediator)
    {
        _mediator = mediator;
    }

    // GET: TestingsController/Create
    [HttpPost]
    public async Task<IActionResult> Create(CreateTestingCommand request)
    {
        var result = await _mediator.Send(request);

        return Ok(result);
    }
}