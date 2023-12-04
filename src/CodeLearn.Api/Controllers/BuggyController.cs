using Microsoft.AspNetCore.Mvc;

namespace CodeLearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BuggyController : ControllerBase
{
    [HttpGet("not-found")]
    public ActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("bad-request")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ProblemDetails { Title = "This is a Bad Request" });
    }

    [HttpGet("unauthorised")]
    public ActionResult GetUnauthorised()
    {
        return Unauthorized();
    }

    [HttpGet("validation-error")]
    public ActionResult GetValidationError()
    {
        ModelState.AddModelError("Problem1", "This is the first Error");
        ModelState.AddModelError("Problem2", "This is the second Error");
        return ValidationProblem();
    }

    [HttpGet("server-error")]
    public ActionResult GetServerError()
    {
        throw new Exception("This is a server Error");
    }
}