using CodeLearn.Application.Exercises.Commands.CreateMethodCodingExercise;
using CodeLearn.Contracts.Exercises.MethodCoding;

namespace CodeLearn.Api.Controllers;

[Route("api/tests/{testId:int}/method-coding-exercises")]
public sealed class TestsMethodCodingExercisesController(ISender sender, IMapper mapper) : ApiControllerBase
{
    //[HttpGet]
    //[ProducesResponseType(typeof(QuestionExerciseResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Get(int testId)
    //{
    //    var result = await sender.Send(new GetExercisesByTestIdQuery(testId));

    //    return result.Match(
    //        test => Ok(mapper.Map<TestResponse>(test)),
    //        _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Test not found."));
    //}

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(int testId, MethodCodingExerciseRequest request)
    {
        var command = mapper.Map<CreateMethodCodingExerciseCommand>((testId, request));
        var result = await sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Not found."),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }
}