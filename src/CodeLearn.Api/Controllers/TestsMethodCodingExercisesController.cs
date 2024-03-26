using CodeLearn.Application.Exercises.Commands.CreateMethodCodingExercise;
using CodeLearn.Application.Exercises.Queries.GetAllMethodCodingExercisesByTestId;
using CodeLearn.Contracts.Exercises.MethodCoding;
using CodeLearn.Domain.Constants;
using System.Security.Claims;

namespace CodeLearn.Api.Controllers;

[Route("api/tests/{testId:int}/method-coding-exercises")]
public sealed class TestsMethodCodingExercisesController(ISender sender, IMapper mapper) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(TeacherMethodCodingExerciseResponseCollection), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(StudentMethodCodingExerciseResponseCollection), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByTestId(int testId)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)!.Value;

        var result = await sender.Send(new GetAllMethodCodingExercisesByTestIdQuery(testId));

        return result.Match(
            exercises => 
            {
                // Map data for teacher / administrator
                if (userRole == Roles.Teacher || userRole == Roles.Administrator)
                {
                    var mappedDataForTeacher = exercises
                        .Select(mapper.Map<TeacherMethodCodingExerciseResponse>)
                        .ToArray();
                    return Ok(new TeacherMethodCodingExerciseResponseCollection(mappedDataForTeacher));
                }

                // Map data for student
                var mappedDataForStudent = exercises
                    .Select(mapper.Map<StudentMethodCodingExerciseResponse>)
                    .ToArray();
                return Ok(new StudentMethodCodingExerciseResponseCollection(mappedDataForStudent));
            },
            _ => Problem(statusCode: StatusCodes.Status404NotFound, title: "Test not found."),
            _ => Problem(statusCode: StatusCodes.Status400BadRequest, title: "Validation failed."));
    }

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