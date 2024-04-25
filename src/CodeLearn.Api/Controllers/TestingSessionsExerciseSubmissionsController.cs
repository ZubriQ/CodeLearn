using CodeLearn.Application.ExerciseSubmissions.MethodCoding.Commands.CreateMethodCodingExerciseSubmission;
using CodeLearn.Application.ExerciseSubmissions.Question.Commands.CreateQuestionExerciseSubmission;
using CodeLearn.Contracts.ExerciseSubmissions.MethodCoding;
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

    [HttpPost("question")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateQuestionSubmission(
        int testingSessionId, [FromBody] QuestionExerciseSubmissionRequest request)
    {
        var command = _mapper.Map<CreateQuestionExerciseSubmissionCommand>((testingSessionId, request));
        var result = await _sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(CreateQuestionSubmission), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Not found."),
            _ => Problem(statusCode: StatusCodes.Status409Conflict, title: "Conflict. Already answered question."));
    }

    [HttpPost("method-coding")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMethodCodingSubmission(
        int testingSessionId, [FromBody] MethodCodingExerciseSubmissionRequest request)
    {
        var command = _mapper.Map<CreateMethodCodingExerciseSubmissionCommand>((testingSessionId, request));
        var result = await _sender.Send(command);

        return result.Match(
            Ok,
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Not found."));
    }
}