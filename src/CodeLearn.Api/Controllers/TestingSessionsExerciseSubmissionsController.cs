using CodeLearn.Application.ExerciseSubmissions.Question.Commands.CreateQuestionExerciseSubmission;
using CodeLearn.Contracts.ExerciseSubmissions.Question;

namespace CodeLearn.Api.Controllers;

[Route("api/testing-sessions/{testingSessionId:int}/exercise-submissions")]
public sealed class TestingSessionsExerciseSubmissionsController(IMapper _mapper, ISender _sender) : ApiControllerBase
{
    //[HttpPost]
    //[ProducesResponseType(typeof(Success), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateQuestionSubmissions(
    //    int testingSessionId, [FromBody] QuestionExerciseSubmissionsRequest request)
    //{
    //    var command = _mapper.Map<CreateQuestionExerciseSubmissionsCommand>((testingSessionId, request));
    //    var result = await _sender.Send(command);

    //    return result.Match(
    //        id => CreatedAtAction(nameof(CreateQuestionSubmissions), new { id }, id),
    //        _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."),
    //        _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Not found."));
    //}

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateQuestionSubmission(
        int testingSessionId, [FromBody] QuestionExerciseSubmissionRequest request)
    {
        var command = _mapper.Map<CreateQuestionExerciseSubmissionCommand>((testingSessionId, request));
        var result = await _sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(CreateQuestionSubmission), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Not found."));
    }
}