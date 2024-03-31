using CodeLearn.Application.DataTypes.Queries.GetAllDataTypes;
using CodeLearn.Contracts.DataTypes;

namespace CodeLearn.Api.Controllers;

public sealed class DataTypesController(ISender _sender, IMapper _mapper) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(DataTypeResponseCollection), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var queryResult = await _sender.Send(new GetAllDataTypesQuery());
        var responseData = queryResult.Select(_mapper.Map<DataTypeResponse>).ToArray();

        return Ok(new DataTypeResponseCollection(responseData));
    }
}