using CodeLearn.Application.Exercises.Commands.CreateQuestionExercise;
using CodeLearn.Application.Exercises.Queries.GetAllQuestionExercises;
using CodeLearn.Contracts.Exercises.Question;

namespace CodeLearn.Api.Controllers;

[Route("api/tests/{testId:int}/question-exercises")]
public sealed class TestsQuestionExercisesController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(QuestionExerciseResponseCollection), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByTestId(int testId)
    {
        var response = await sender.Send(new GetAllQuestionExercisesByTestIdQuery(testId));
        var mappedData = response.Select(mapper.Map<QuestionExerciseResponse>).ToArray();

        return Ok(new QuestionExerciseResponseCollection(mappedData));
    }

    [HttpPost]
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
}