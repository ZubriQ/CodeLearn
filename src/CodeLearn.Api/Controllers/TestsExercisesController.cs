using CodeLearn.Application.Exercises.Commands.CreateMethodCodingExercise;
using CodeLearn.Application.Exercises.Commands.CreateQuestionExercise;
using CodeLearn.Contracts.Exercises.MethodCoding;
using CodeLearn.Contracts.Exercises.Question;

namespace CodeLearn.Api.Controllers;

[Route("api/tests/{testId:int}/exercises")]
public sealed class TestsExercisesController(ISender sender, IMapper mapper) : ApiControllerBase
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

    [HttpPost("add-question")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(int testId, QuestionExerciseRequest request)
    {
        var command = mapper.Map<CreateQuestionExerciseCommand>((testId, request));
        var result = await sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }

    [HttpPost("add-method-coding")]
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