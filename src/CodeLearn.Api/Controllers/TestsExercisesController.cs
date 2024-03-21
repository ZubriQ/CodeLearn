using CodeLearn.Application.Tests.Queries.GetTestById;
using CodeLearn.Contracts.Tests;

namespace CodeLearn.Api.Controllers;

[Route("api/tests/{testId:int}/exercises")]
public sealed class TestsExercisesController(ISender sender, IMapper mapper) : ApiControllerBase
{
    //[HttpGet("{testId:int}")]
    //[ProducesResponseType(typeof(TestResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Get(int testId)
    //{
    //    var result = await sender.Send(new GetExercisesByTestIdQuery(testId));

    //    return result.Match(
    //        test => Ok(mapper.Map<TestResponse>(test)),
    //        _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Test not found."));
    //}
}