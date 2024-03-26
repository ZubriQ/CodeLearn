using CodeLearn.Application.Exercises.Commands.CreateQuestionExercise;
using CodeLearn.Application.Exercises.Queries.GetAllQuestionExercisesByTestId;
using CodeLearn.Contracts.Exercises.Question;
using CodeLearn.Domain.Constants;
using System.Security.Claims;

namespace CodeLearn.Api.Controllers;

[Route("api/tests/{testId:int}/question-exercises")]
public sealed class TestsQuestionExercisesController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(TeacherQuestionExerciseResponseCollection), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(StudentQuestionExerciseResponseCollection), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByTestIdForTeacher(int testId)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        var result = await sender.Send(new GetAllQuestionExercisesByTestIdQuery(testId));

        return result.Match(
            exercises =>
            {
                // Map data for teacher / administrator
                if (userRole == Roles.Teacher || userRole == Roles.Administrator)
                {
                    var mappedDataForTeacher = exercises
                        .Select(mapper.Map<TeacherQuestionExerciseResponse>)
                        .ToArray();
                    return Ok(new TeacherQuestionExerciseResponseCollection(mappedDataForTeacher));
                }

                // Map data for student
                var mappedDataForStudent = exercises
                    .Select(mapper.Map<StudentQuestionExerciseResponse>)
                    .ToArray();
                return Ok(new StudentQuestionExerciseResponseCollection(mappedDataForStudent));
            },
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Test not found."),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
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