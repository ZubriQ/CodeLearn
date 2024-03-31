using CodeLearn.Application.ExerciseTopics.Queries.GetAllExerciseTopics;
using CodeLearn.Contracts.ExerciseTopics;

namespace CodeLearn.Api.Controllers;

public sealed class ExerciseTopicsController(ISender _sender, IMapper _mapper) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ExerciseTopicResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var queryResult = await _sender.Send(new GetAllExerciseTopicsQuery());
        var responseData = queryResult.Select(_mapper.Map<ExerciseTopicResponse>).ToArray();

        return Ok(new ExerciseTopicResponseCollection(responseData));
    }
}