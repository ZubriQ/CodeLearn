using CodeLearn.Application.Exercises.Commands.DeleteExercise;
using CodeLearn.Application.Exercises.MethodCodingExercises.Queries.GetMethodCodingExerciseById;
using CodeLearn.Application.Exercises.QuestionExercises.Queries.GetQuestionExerciseById;
using CodeLearn.Contracts.Exercises.MethodCoding;
using CodeLearn.Contracts.Exercises.Question;

namespace CodeLearn.Api.Controllers;

public sealed class ExercisesController(ISender _sender, IMapper _mapper) : ApiControllerBase
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

    [HttpGet("method-coding/{exerciseId:int}")]
    [ProducesResponseType(typeof(StudentMethodCodingExerciseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMethodCodingExerciseById(int exerciseId)
    {
        var result = await _sender.Send(new GetMethodCodingExerciseByIdQuery(exerciseId));

        return result.Match(
            exercise => Ok(_mapper.Map<StudentMethodCodingExerciseResponse>(exercise)),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Exercise not found."));
    }

    [HttpGet("question/{exerciseId:int}")]
    [ProducesResponseType(typeof(StudentQuestionExerciseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetQuestionExerciseById(int exerciseId)
    {
        var result = await _sender.Send(new GetQuestionExerciseByIdQuery(exerciseId));

        return result.Match(
            exercise => Ok(_mapper.Map<StudentQuestionExerciseResponse>(exercise)),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Exercise not found."));
    }
}