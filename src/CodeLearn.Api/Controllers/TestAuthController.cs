using Microsoft.AspNetCore.Authorization;

namespace CodeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TestAuthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new[] { 1, 2, 3, 4 });
    }
}