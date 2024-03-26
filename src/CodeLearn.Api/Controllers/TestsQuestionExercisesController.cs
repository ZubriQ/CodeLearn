using CodeLearn.Application.Exercises.Commands.CreateQuestionExercise;
using CodeLearn.Contracts.Exercises.Question;

namespace CodeLearn.Api.Controllers;

[Route("api/tests/{testId:int}/question-exercises")]
public class TestsQuestionExercisesController(ISender sender, IMapper mapper) : ApiControllerBase
{
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
