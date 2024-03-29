using CodeLearn.Application.Exercises.Commands.DeleteExerecise;

namespace CodeLearn.Api.Controllers;

public sealed class ExercisesController(ISender _sender) : ApiControllerBase
{
    [HttpDelete("{exerciseId:int}")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int exerciseId)
    {
        var result = await _sender.Send(new DeleteExerciseCommand(exerciseId));

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Exercise not found."),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Invalid id value."));
    }
}