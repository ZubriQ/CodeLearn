﻿using CodeLearn.Application.TestingSessions.Commands.CreateTestingSession;
using CodeLearn.Application.TestingSessions.Commands.FinishTestingSession;
using CodeLearn.Application.TestingSessions.Queries.GetAllMyTestingSessions;
using CodeLearn.Application.TestingSessions.Queries.GetCompletedExerciseIdsById;
using CodeLearn.Application.TestingSessions.Queries.GetTestingSessionById;
using CodeLearn.Contracts.TestingSessions;
using System.Security.Claims;

namespace CodeLearn.Api.Controllers;

public sealed class TestingSessionsController(ISender _sender, IMapper _mapper) : ApiControllerBase
{
    [HttpGet("{testingSessionId:int}")]
    [ProducesResponseType(typeof(TestingSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int testingSessionId)
    {
        var result = await _sender.Send(new GetTestingSessionByIdQuery(testingSessionId));

        return result.Match(
            testingSession => Ok(_mapper.Map<TestingSessionResponse>(testingSession)),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Testing session not found."));
    }

    [HttpGet("my-sessions")]
    [ProducesResponseType(typeof(StudentTestingSessionResponseCollection), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllForStudentCurriculum() // TODO: for a specific testing sessions*? or in another endpoint
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        var query = new GetAllMyTestingSessionsQuery(userId);
        var response = await _sender.Send(query);

        return response.Match(
            testingSessions =>
            {
                var mappedData = testingSessions.Select(_mapper.Map<StudentTestingSessionResponse>).ToArray();
                return Ok(new StudentTestingSessionResponseCollection(mappedData));
            },
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Not found."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(TestingSessionRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        if (userId == null)
        {
            return Unauthorized();
        }

        var command = _mapper.Map<CreateTestingSessionCommand>((request, userId));
        var result = await _sender.Send(command);

        return result.Match(
            id => CreatedAtAction(nameof(Create), new { id }, id),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Testing not found."),
            _ => Problem(statusCode: StatusCodes.Status409Conflict, title: "Testing session already exists."));
    }

    [HttpGet("{testingSessionId:int}/completed-exercises")]
    [ProducesResponseType(typeof(int[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAnsweredQuestionsById(int testingSessionId)
    { // TODO: gets both completed exercises and questions
        var result = await _sender.Send(new GetCompletedExerciseIdsByIdQuery(testingSessionId));

        return result.Match(
            Ok,
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Testing session not found."));
    }

    [HttpPost("{testingSessionId:int}/finish")]
    [ProducesResponseType(typeof(Success), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FinishTestingSession(int testingSessionId, [FromBody] FinishTestingSessionRequest request)
    {
        var result = await _sender.Send(new FinishTestingSessionCommand(testingSessionId, request.StudentFeedback));

        return result.Match(
            success => Ok(success),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Bad request."),
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Testing session not found."));
    }
}