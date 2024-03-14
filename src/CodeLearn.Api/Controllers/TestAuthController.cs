//using CodeLearn.Application.Common.Interfaces;
//using CodeLearn.Domain.ExerciseTopics;

//namespace CodeLearn.Api.Controllers;

//[Route("api/[controller]")]
//[ApiController]
////[Authorize]
//public class TestAuthController : ControllerBase
//{
//    private readonly IApplicationDbContext _service;

//    public TestAuthController(IApplicationDbContext service)
//    {
//        _service = service;
//    }

//    [HttpGet]
//    public async Task<IActionResult> Get()
//    {
//        var topic = ExerciseTopic.Create("My topic");

//        _service.ExerciseTopics.Add(topic);

//        await _service.SaveChangesAsync(CancellationToken.None);

//        return Ok(new[] { 1, 2, 3, 4 });
//    }

//    //[HttpGet]
//    //public async Task<IActionResult> Update()
//    //{
//    //    var topics = _service.ExerciseTopics.ToList();

//    //    //await _service.SaveChangesAsync(CancellationToken.None);

//    //    return Ok(new[] { 1, 2, 3, 4 });
//    //}
//}